using GTANetworkAPI;
using WiredPlayers.database;
using WiredPlayers.globals;
using WiredPlayers.model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace WiredPlayers.drivingschool
{
    public class DrivingSchool : Script
    {
        private static Dictionary<int, Timer> drivingSchoolTimerList = new Dictionary<int, Timer>();


        public static void OnPlayerDisconnected(Client player, DisconnectionType type, string reason)
        {
            if (drivingSchoolTimerList.TryGetValue(player.Value, out Timer drivingSchoolTimer) == true)
            {
                // We remove the timer
                drivingSchoolTimer.Dispose();
                drivingSchoolTimerList.Remove(player.Value);
            }
        }

        private void OnDrivingTimer(object playerObject)
        {
            // We get the player and his vehicle
            Client player = (Client)playerObject;
            Vehicle vehicle = player.GetData(EntityData.PLAYER_VEHICLE);

            // We finish the exam
            FinishDrivingExam(player, vehicle);

            // Deleting timer from the list
            if (drivingSchoolTimerList.TryGetValue(player.Value, out Timer drivingSchoolTimer) == true)
            {
                drivingSchoolTimer.Dispose();
                drivingSchoolTimerList.Remove(player.Value);
            }

            // Confirmation message sent to the player
            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_LICENSE_FAILED_NOT_IN_VEHICLE);
        }

        private void FinishDrivingExam(Client player, Vehicle vehicle)
        {
            // Vehicle reseting
            vehicle.Repair();
            vehicle.Position = vehicle.GetData(EntityData.VEHICLE_POSITION);
            vehicle.Rotation = vehicle.GetData(EntityData.VEHICLE_ROTATION);

            // Checkpoint delete
            if (NAPI.Vehicle.GetVehicleDriver(vehicle) == player)
            {
                Checkpoint licenseCheckpoint = player.GetData(EntityData.PLAYER_DRIVING_COLSHAPE);
                player.TriggerEvent("deleteLicenseCheckpoint");
                licenseCheckpoint.Delete();
            }

            // Entity data cleanup
            player.ResetData(EntityData.PLAYER_VEHICLE);
            player.ResetData(EntityData.PLAYER_DRIVING_EXAM);
            player.ResetData(EntityData.PLAYER_DRIVING_COLSHAPE);
            player.ResetData(EntityData.PLAYER_DRIVING_CHECKPOINT);

            // Remove player from vehicle
            player.WarpOutOfVehicle();
        }

        public static int GetPlayerLicenseStatus(Client player, int license)
        {
            string playerLicenses = player.GetData(EntityData.PLAYER_LICENSES);
            string[] licenses = playerLicenses.Split(',');
            return int.Parse(licenses[license]);
        }

        public static void SetPlayerLicense(Client player, int license, int value)
        {
            // We get player licenses
            string playerLicenses = player.GetData(EntityData.PLAYER_LICENSES);
            string[] licenses = playerLicenses.Split(',');

            // Changing license status
            licenses[license] = value.ToString();
            playerLicenses = string.Join(",", licenses);

            // Save the new licenses
            player.SetData(EntityData.PLAYER_LICENSES, playerLicenses);
        }

        [ServerEvent(Event.PlayerEnterVehicle)]
        public void OnPlayerEnterVehicle(Client player, Vehicle vehicle, sbyte seatId)
        {
            if (vehicle.GetData(EntityData.VEHICLE_FACTION) == Constants.FACTION_DRIVING_SCHOOL)
            {
                VehicleHash vehicleHash = (VehicleHash) vehicle.Model;
                if (player.HasData(EntityData.PLAYER_DRIVING_EXAM) && player.GetData(EntityData.PLAYER_DRIVING_EXAM) == Constants.CAR_DRIVING_PRACTICE)
                {
                    // We check the class of the vehicle
                    if (NAPI.Vehicle.GetVehicleClass(vehicleHash) == Constants.VEHICLE_CLASS_SEDANS)
                    {
                        int checkPoint = player.GetData(EntityData.PLAYER_DRIVING_CHECKPOINT);
                        if (drivingSchoolTimerList.TryGetValue(player.Value, out Timer drivingSchoolTimer) == true)
                        {
                            drivingSchoolTimer.Dispose();
                            drivingSchoolTimerList.Remove(player.Value);
                        }
                        Checkpoint newCheckpoint = NAPI.Checkpoint.CreateCheckpoint(0, Constants.CAR_LICENSE_CHECKPOINTS[checkPoint], Constants.CAR_LICENSE_CHECKPOINTS[checkPoint + 1], 2.5f, new Color(198, 40, 40, 200));
                        player.SetData(EntityData.PLAYER_DRIVING_COLSHAPE, newCheckpoint);
                        player.SetData(EntityData.PLAYER_VEHICLE, vehicle);

                        // We place a mark on the map
                        player.TriggerEvent("showLicenseCheckpoint", Constants.CAR_LICENSE_CHECKPOINTS[checkPoint]);
                    }
                    else
                    {
                        player.WarpOutOfVehicle();
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_VEHICLE_DRIVING_NOT_SUITABLE);
                    }
                }
                else if (player.HasData(EntityData.PLAYER_DRIVING_EXAM) && player.GetData(EntityData.PLAYER_DRIVING_EXAM) == Constants.MOTORCYCLE_DRIVING_PRACTICE)
                {
                    // We check the class of the vehicle
                    if (NAPI.Vehicle.GetVehicleClass(vehicleHash) == Constants.VEHICLE_CLASS_MOTORCYCLES)
                    {
                        int checkPoint = player.GetData(EntityData.PLAYER_DRIVING_CHECKPOINT);
                        if (drivingSchoolTimerList.TryGetValue(player.Value, out Timer drivingSchoolTimer) == true)
                        {
                            drivingSchoolTimer.Dispose();
                            drivingSchoolTimerList.Remove(player.Value);
                        }
                        Checkpoint newCheckpoint = NAPI.Checkpoint.CreateCheckpoint(0, Constants.BIKE_LICENSE_CHECKPOINTS[checkPoint], Constants.BIKE_LICENSE_CHECKPOINTS[checkPoint + 1], 2.5f, new Color(198, 40, 40, 200));
                        player.SetData(EntityData.PLAYER_DRIVING_COLSHAPE, newCheckpoint);
                        player.SetData(EntityData.PLAYER_VEHICLE, vehicle);

                        // We place a mark on the map
                        player.TriggerEvent("showLicenseCheckpoint", Constants.BIKE_LICENSE_CHECKPOINTS[checkPoint]);
                    }
                    else
                    {
                        player.WarpOutOfVehicle();
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_VEHICLE_DRIVING_NOT_SUITABLE);
                    }
                }
                else
                {
                    player.WarpOutOfVehicle();
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_IN_CAR_PRACTICE);
                }
            }
        }

        [ServerEvent(Event.PlayerExitVehicle)]
        public void OnPlayerExitVehicle(Client player, Vehicle vehicle)
        {
            if (player.HasData(EntityData.PLAYER_DRIVING_EXAM) && player.HasData(EntityData.PLAYER_VEHICLE) == true)
            {
                // Checking if is a valid vehicle
                if (player.GetData(EntityData.PLAYER_VEHICLE) == vehicle && vehicle.GetData(EntityData.VEHICLE_FACTION) == Constants.FACTION_DRIVING_SCHOOL)
                {
                    string warn = string.Format(Messages.INF_LICENSE_VEHICLE_EXIT, 15);
                    Checkpoint playerDrivingCheckpoint = player.GetData(EntityData.PLAYER_DRIVING_COLSHAPE);
                    playerDrivingCheckpoint.Delete();
                    player.SendChatMessage(Constants.COLOR_INFO + warn);

                    // Removing the checkpoint marker
                    player.TriggerEvent("deleteLicenseCheckpoint");

                    // When the timer finishes, the exam will be failed
                    Timer drivingSchoolTimer = new Timer(OnDrivingTimer, player, 15000, Timeout.Infinite);
                    drivingSchoolTimerList.Add(player.Value, drivingSchoolTimer);
                }
            }
        }

        [ServerEvent(Event.PlayerEnterCheckpoint)]
        public void OnPlayerEnterCheckpoint(Checkpoint checkpoint, Client player)
        {
            if (player.HasData(EntityData.PLAYER_DRIVING_COLSHAPE) && player.HasData(EntityData.PLAYER_DRIVING_EXAM) == true)
            {
                if (player.IsInVehicle && player.GetData(EntityData.PLAYER_DRIVING_EXAM) == Constants.CAR_DRIVING_PRACTICE)
                {
                    Vehicle vehicle = player.Vehicle;
                    if (checkpoint == player.GetData(EntityData.PLAYER_DRIVING_COLSHAPE) && vehicle.GetData(EntityData.VEHICLE_FACTION) == Constants.FACTION_DRIVING_SCHOOL)
                    {
                        Checkpoint currentCheckpoint = player.GetData(EntityData.PLAYER_DRIVING_COLSHAPE);
                        int checkPoint = player.GetData(EntityData.PLAYER_DRIVING_CHECKPOINT);

                        if (checkPoint < Constants.CAR_LICENSE_CHECKPOINTS.Count - 2)
                        {
                            currentCheckpoint.Position = Constants.CAR_LICENSE_CHECKPOINTS[checkPoint + 1];
                            currentCheckpoint.Direction = Constants.CAR_LICENSE_CHECKPOINTS[checkPoint + 2];
                            player.SetData(EntityData.PLAYER_DRIVING_CHECKPOINT, checkPoint + 1);

                            // We place a mark on the map
                            player.TriggerEvent("showLicenseCheckpoint", currentCheckpoint.Position);
                        }
                        else if (checkPoint == Constants.CAR_LICENSE_CHECKPOINTS.Count - 2)
                        {
                            currentCheckpoint.Position = Constants.CAR_LICENSE_CHECKPOINTS[checkPoint + 1];
                            currentCheckpoint.Direction = vehicle.GetData(EntityData.VEHICLE_POSITION);
                            player.SetData(EntityData.PLAYER_DRIVING_CHECKPOINT, checkPoint + 1);

                            // We place a mark on the map
                            player.TriggerEvent("showLicenseCheckpoint", currentCheckpoint.Position);
                        }
                        else if (checkPoint == Constants.CAR_LICENSE_CHECKPOINTS.Count - 1)
                        {
                            currentCheckpoint.Position = vehicle.GetData(EntityData.VEHICLE_POSITION);
                            NAPI.Entity.SetEntityModel(currentCheckpoint, (int)CheckpointType.Checkerboard);
                            player.SetData(EntityData.PLAYER_DRIVING_CHECKPOINT, checkPoint + 1);

                            // We place a mark on the map
                            player.TriggerEvent("showLicenseCheckpoint", currentCheckpoint.Position);
                        }
                        else
                        {
                            // Exam finished
                            FinishDrivingExam(player, vehicle);

                            // We add points to the license
                            SetPlayerLicense(player, Constants.LICENSE_CAR, 12);

                            // Confirmation message sent to the player
                            player.SendChatMessage(Constants.COLOR_SUCCESS + Messages.SUC_LICENSE_DRIVE_PASSED);
                        }
                    }
                }
                else if (player.IsInVehicle && player.GetData(EntityData.PLAYER_DRIVING_EXAM) == Constants.MOTORCYCLE_DRIVING_PRACTICE)
                {
                    Vehicle vehicle = player.Vehicle;
                    if (checkpoint == player.GetData(EntityData.PLAYER_DRIVING_COLSHAPE) && vehicle.GetData(EntityData.VEHICLE_FACTION) == Constants.FACTION_DRIVING_SCHOOL)
                    {
                        Checkpoint currentCheckpoint = player.GetData(EntityData.PLAYER_DRIVING_COLSHAPE);
                        int checkPoint = player.GetData(EntityData.PLAYER_DRIVING_CHECKPOINT);

                        if (checkPoint < Constants.BIKE_LICENSE_CHECKPOINTS.Count - 2)
                        {
                            currentCheckpoint.Position = Constants.BIKE_LICENSE_CHECKPOINTS[checkPoint + 1];
                            currentCheckpoint.Direction = Constants.BIKE_LICENSE_CHECKPOINTS[checkPoint + 2];
                            player.SetData(EntityData.PLAYER_DRIVING_CHECKPOINT, checkPoint + 1);

                            // We place a mark on the map
                            player.TriggerEvent("showLicenseCheckpoint", currentCheckpoint.Position);
                        }
                        else if (checkPoint == Constants.BIKE_LICENSE_CHECKPOINTS.Count - 2)
                        {
                            currentCheckpoint.Position = Constants.BIKE_LICENSE_CHECKPOINTS[checkPoint + 1];
                            currentCheckpoint.Direction = vehicle.GetData(EntityData.VEHICLE_POSITION);
                            player.SetData(EntityData.PLAYER_DRIVING_CHECKPOINT, checkPoint + 1);

                            // We place a mark on the map
                            player.TriggerEvent("showLicenseCheckpoint", currentCheckpoint.Position);
                        }
                        else if (checkPoint == Constants.BIKE_LICENSE_CHECKPOINTS.Count - 1)
                        {
                            currentCheckpoint.Position = vehicle.GetData(EntityData.VEHICLE_POSITION);
                            NAPI.Entity.SetEntityModel(currentCheckpoint, (int)CheckpointType.Checkerboard);
                            player.SetData(EntityData.PLAYER_DRIVING_CHECKPOINT, checkPoint + 1);

                            // We place a mark on the map
                            player.TriggerEvent("showLicenseCheckpoint", currentCheckpoint.Position);
                        }
                        else
                        {
                            // Exam finished
                            FinishDrivingExam(player, vehicle);

                            // We add points to the license
                            SetPlayerLicense(player, Constants.LICENSE_MOTORCYCLE, 12);

                            // Confirmation message sent to the player
                            player.SendChatMessage(Constants.COLOR_SUCCESS + Messages.SUC_LICENSE_DRIVE_PASSED);
                        }
                    }
                }
            }
        }

        [ServerEvent(Event.VehicleDamage)]
        public void OnVehicleDamage(Vehicle vehicle, float lossFirst, float lossSecond)
        {
            Client player = NAPI.Vehicle.GetVehicleDriver(vehicle);
            if (player != null && player.HasData(EntityData.PLAYER_DRIVING_COLSHAPE) && player.HasData(EntityData.PLAYER_DRIVING_EXAM) == true)
            {
                if (lossFirst - vehicle.Health > 5.0f)
                {
                    // Exam finished
                    FinishDrivingExam(player, vehicle);

                    // Inform the player about his failure
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_LICENSE_DRIVE_FAILED);
                }
            }
        }

        [ServerEvent(Event.Update)]
        public void OnUpdate()
        {
            foreach (Client player in NAPI.Pools.GetAllPlayers())
            {
                if (player.HasData(EntityData.PLAYER_PLAYING) && player.HasData(EntityData.PLAYER_DRIVING_EXAM) == true)
                {
                    // Check if is driving a vehicle
                    if (player.IsInVehicle && player.VehicleSeat == (int)VehicleSeat.Driver)
                    {
                        Vehicle vehicle = player.Vehicle;
                        if (vehicle.GetData(EntityData.VEHICLE_FACTION) == Constants.FACTION_DRIVING_SCHOOL)
                        {
                            Vector3 velocity = NAPI.Entity.GetEntityVelocity(vehicle);
                            double speed = Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y + velocity.Z * velocity.Z);
                            if (Math.Round(speed * 3.6f) > Constants.MAX_DRIVING_VEHICLE)
                            {
                                // Exam finished
                                FinishDrivingExam(player, vehicle);

                                // Inform the player about his failure
                                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_LICENSE_DRIVE_FAILED);
                            }
                        }
                    }
                }
            }
        }

        [RemoteEvent("checkAnswer")]
        public void CheckAnswerEvent(Client player, int answer)
        {
            Task.Factory.StartNew(() =>
            {
                if (Database.CheckAnswerCorrect(answer) == true)
                {
                    // We add the correct answer
                    int nextQuestion = player.GetSharedData(EntityData.PLAYER_LICENSE_QUESTION) + 1;

                    if (nextQuestion < Constants.MAX_LICENSE_QUESTIONS)
                    {
                        // Go for the next question
                        player.SetSharedData(EntityData.PLAYER_LICENSE_QUESTION, nextQuestion);
                        player.TriggerEvent("getNextTestQuestion");
                    }
                    else
                    {
                        // Player passed the exam
                        int license = player.GetData(EntityData.PLAYER_LICENSE_TYPE);
                        SetPlayerLicense(player, license, 0);

                        // Reset the entity data
                        player.ResetData(EntityData.PLAYER_LICENSE_TYPE);
                        player.ResetSharedData(EntityData.PLAYER_LICENSE_QUESTION);

                        // Send the message to the player
                        player.SendChatMessage(Constants.COLOR_SUCCESS + Messages.SUC_LICENSE_EXAM_PASSED);

                        // Exam window close
                        player.TriggerEvent("finishLicenseExam");
                    }
                }
                else
                {
                    // Player failed the exam
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_LICENSE_EXAM_FAILED);

                    // Reset the entity data
                    player.ResetData(EntityData.PLAYER_LICENSE_TYPE);
                    player.ResetSharedData(EntityData.PLAYER_LICENSE_QUESTION);

                    // Exam window close
                    player.TriggerEvent("finishLicenseExam");
                }
            });
        }

        [Command(Messages.COM_DRIVING_SCHOOL, Messages.GEN_DRIVING_SCHOOL_COMMAND)]
        public void DrivingSchoolCommand(Client player, string type)
        {
            int licenseStatus = 0;
            foreach (InteriorModel interior in Constants.INTERIOR_LIST)
            {
                if (interior.captionMessage == Messages.GEN_DRIVING_SCHOOL && player.Position.DistanceTo(interior.entrancePosition) < 2.5f)
                {
                    List<TestModel> questions = new List<TestModel>();
                    List<TestModel> answers = new List<TestModel>();

                    // Get the player's money
                    int money = player.GetSharedData(EntityData.PLAYER_MONEY);

                    switch (type.ToLower())
                    {
                        case Messages.ARG_CAR:
                            // Check for the status if the license
                            licenseStatus = GetPlayerLicenseStatus(player, Constants.LICENSE_CAR);

                            switch (licenseStatus)
                            {
                                case -1:
                                    // Check if the player has enough money
                                    if (money >= Constants.PRICE_DRIVING_THEORICAL)
                                    {
                                        Task.Factory.StartNew(() =>
                                        {
                                            // Add the questions
                                            questions = Database.GetRandomQuestions(Constants.LICENSE_CAR + 1);
                                            foreach (TestModel question in questions)
                                            {
                                                answers.AddRange(Database.GetQuestionAnswers(question.id));
                                            }

                                            player.SetData(EntityData.PLAYER_LICENSE_TYPE, Constants.LICENSE_CAR);
                                            player.SetSharedData(EntityData.PLAYER_LICENSE_QUESTION, 0);

                                            player.SetSharedData(EntityData.PLAYER_MONEY, money - Constants.PRICE_DRIVING_THEORICAL);

                                            // Start the exam
                                            player.TriggerEvent("startLicenseExam", NAPI.Util.ToJson(questions), NAPI.Util.ToJson(answers));
                                        });
                                    }
                                    else
                                    {
                                        string message = string.Format(Messages.ERR_DRIVING_SCHOOL_MONEY, Constants.PRICE_DRIVING_THEORICAL);
                                        player.SendChatMessage(Constants.COLOR_ERROR + message);
                                    }
                                    break;
                                case 0:
                                    // Check if the player has enough money
                                    if (money >= Constants.PRICE_DRIVING_PRACTICAL)
                                    {
                                        player.SetData(EntityData.PLAYER_LICENSE_TYPE, Constants.LICENSE_CAR);
                                        player.SetData(EntityData.PLAYER_DRIVING_EXAM, Constants.CAR_DRIVING_PRACTICE);
                                        player.SetData(EntityData.PLAYER_DRIVING_CHECKPOINT, 0);

                                        player.SetSharedData(EntityData.PLAYER_MONEY, money - Constants.PRICE_DRIVING_PRACTICAL);

                                        player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_ENTER_LICENSE_CAR_VEHICLE);
                                    }
                                    else
                                    {
                                        string message = string.Format(Messages.ERR_DRIVING_SCHOOL_MONEY, Constants.PRICE_DRIVING_PRACTICAL);
                                        player.SendChatMessage(Constants.COLOR_ERROR + message);
                                    }
                                    break;
                                default:
                                    // License up to date
                                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_ALREADY_LICENSE);
                                    break;
                            }
                            break;
                        case Messages.ARG_MOTORCYCLE:
                            // Check for the status if the license
                            licenseStatus = GetPlayerLicenseStatus(player, Constants.LICENSE_MOTORCYCLE);

                            switch (licenseStatus)
                            {
                                case -1:
                                    // Check if the player has enough money
                                    if (money >= Constants.PRICE_DRIVING_THEORICAL)
                                    {
                                        Task.Factory.StartNew(() =>
                                        {
                                            // Add the questions
                                            questions = Database.GetRandomQuestions(Constants.LICENSE_MOTORCYCLE + 1);
                                            foreach (TestModel question in questions)
                                            {
                                                answers.AddRange(Database.GetQuestionAnswers(question.id));
                                            }

                                            player.SetData(EntityData.PLAYER_LICENSE_TYPE, Constants.LICENSE_MOTORCYCLE);
                                            player.SetSharedData(EntityData.PLAYER_LICENSE_QUESTION, 0);

                                            player.SetSharedData(EntityData.PLAYER_MONEY, money - Constants.PRICE_DRIVING_THEORICAL);

                                            // Start the exam
                                            player.TriggerEvent("startLicenseExam", NAPI.Util.ToJson(questions), NAPI.Util.ToJson(answers));
                                        });
                                    }
                                    else
                                    {
                                        string message = string.Format(Messages.ERR_DRIVING_SCHOOL_MONEY, Constants.PRICE_DRIVING_THEORICAL);
                                        player.SendChatMessage(Constants.COLOR_ERROR + message);
                                    }
                                    break;
                                case 0:
                                    // Check if the player has enough money
                                    if (money >= Constants.PRICE_DRIVING_PRACTICAL)
                                    {
                                        player.SetData(EntityData.PLAYER_LICENSE_TYPE, Constants.LICENSE_MOTORCYCLE);
                                        player.SetData(EntityData.PLAYER_DRIVING_EXAM, Constants.MOTORCYCLE_DRIVING_PRACTICE);
                                        player.SetData(EntityData.PLAYER_DRIVING_CHECKPOINT, 0);

                                        player.SetSharedData(EntityData.PLAYER_MONEY, money - Constants.PRICE_DRIVING_PRACTICAL);

                                        player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_ENTER_LICENSE_BIKE_VEHICLE);
                                    }
                                    else
                                    {
                                        string message = string.Format(Messages.ERR_DRIVING_SCHOOL_MONEY, Constants.PRICE_DRIVING_PRACTICAL);
                                        player.SendChatMessage(Constants.COLOR_ERROR + message);
                                    }
                                    break;
                                default:
                                    // License up to date
                                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_ALREADY_LICENSE);
                                    break;
                            }
                            break;
                        default:
                            player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_DRIVING_SCHOOL_COMMAND);
                            break;
                    }
                    return;
                }
            }

            // Player's not in the driving school
            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_DRIVING_SCHOOL);
        }

        [Command(Messages.COM_LICENSES)]
        public void LicensesCommand(Client player)
        {
            int currentLicense = 0;
            string playerLicenses = player.GetData(EntityData.PLAYER_LICENSES);
            string[] playerLicensesArray = playerLicenses.Split(',');
            player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_LICENSE_LIST);
            foreach (string license in playerLicensesArray)
            {
                int currentLicenseStatus = int.Parse(license);
                switch (currentLicense)
                {
                    case Constants.LICENSE_CAR:
                        switch (currentLicenseStatus)
                        {
                            case -1:
                                player.SendChatMessage(Constants.COLOR_HELP + Messages.INF_CAR_LICENSE_NOT_AVAILABLE);
                                break;
                            case 0:
                                player.SendChatMessage(Constants.COLOR_HELP + Messages.INF_CAR_LICENSE_PRACTICAL_PENDING);
                                break;
                            default:
                                string message = string.Format(Messages.INF_CAR_LICENSE_POINTS + currentLicenseStatus);
                                player.SendChatMessage(Constants.COLOR_HELP + message);
                                break;
                        }
                        break;
                    case Constants.LICENSE_MOTORCYCLE:
                        switch (currentLicenseStatus)
                        {
                            case -1:
                                player.SendChatMessage(Constants.COLOR_HELP + Messages.INF_MOTORCYCLE_LICENSE_NOT_AVAILABLE);
                                break;
                            case 0:
                                player.SendChatMessage(Constants.COLOR_HELP + Messages.INF_MOTORCYCLE_LICENSE_PRACTICAL_PENDING);
                                break;
                            default:
                                string message = string.Format(Messages.INF_MOTORCYCLE_LICENSE_POINTS + currentLicenseStatus);
                                player.SendChatMessage(Constants.COLOR_HELP + message);
                                break;
                        }
                        break;
                    case Constants.LICENSE_TAXI:
                        if (currentLicenseStatus == -1)
                        {
                            player.SendChatMessage(Constants.COLOR_HELP + Messages.INF_TAXI_LICENSE_NOT_AVAILABLE);
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_HELP + Messages.INF_TAXI_LICENSE_UP_TO_DATE);
                        }
                        break;
                }
                currentLicense++;
            }
        }
    }
}
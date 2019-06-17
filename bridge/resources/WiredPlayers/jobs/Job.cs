using GTANetworkAPI;
using WiredPlayers.globals;
using WiredPlayers.model;
using System.Collections.Generic;
using WiredPlayers.character;

namespace WiredPlayers.jobs
{
    public class Job : Script
    {
        private List<JobPickModel> jobList = new List<JobPickModel>()
        {
            new JobPickModel(Constants.JOB_FASTFOOD, new Vector3(-1037.697f, -1397.189f, 5.553192f), Messages.DESC_JOB_FASTFOOT),
            new JobPickModel(Constants.JOB_HOOKER, new Vector3(136.58f, -1278.55f, 29.45f), Messages.DESC_JOB_HOOKER),
            new JobPickModel(Constants.JOB_GARBAGE, new Vector3(-322.088f, -1546.014f, 31.01991f), Messages.DESC_JOB_GARBAGE),
            new JobPickModel(Constants.JOB_MECHANIC, new Vector3(486.5268f, -1314.683f, 29.22961f), Messages.DESC_JOB_MECHANIC),
            new JobPickModel(Constants.JOB_THIEF, new Vector3(-198.225f, -1699.521f, 33.46679f), Messages.DESC_JOB_THIEF)
        };

        public static int GetJobPoints(Client player, int job)
        {
            string jobPointsString = player.GetData(EntityData.PLAYER_JOB_POINTS);
            return int.Parse(jobPointsString.Split(',')[job]);
        }

        public static void SetJobPoints(Client player, int job, int points)
        {
            string jobPointsString = player.GetData(EntityData.PLAYER_JOB_POINTS);
            string[] jobPointsArray = jobPointsString.Split(',');
            jobPointsArray[job] = points.ToString();
            jobPointsString = string.Join(",", jobPointsArray);
            player.SetData(EntityData.PLAYER_JOB_POINTS, jobPointsString);
        }

        [ServerEvent(Event.ResourceStart)]
        public void OnResourceStart()
        {
            Blip trashBlip = NAPI.Blip.CreateBlip(new Vector3(-322.088f, -1546.014f, 31.01991f));
            trashBlip.Name = Messages.GEN_GARBAGE_JOB;
            trashBlip.ShortRange = true;
            trashBlip.Sprite = 318;

            Blip mechanicBlip = NAPI.Blip.CreateBlip(new Vector3(486.5268f, -1314.683f, 29.22961f));
            mechanicBlip.Name = Messages.GEN_MECHANIC_JOB;
            mechanicBlip.ShortRange = true;
            mechanicBlip.Sprite = 72;

            Blip fastFoodBlip = NAPI.Blip.CreateBlip(new Vector3(-1037.697f, -1397.189f, 5.553192f));
            fastFoodBlip.Name = Messages.GEN_FASTFOOD_JOB;
            fastFoodBlip.ShortRange = true;
            fastFoodBlip.Sprite = 501;

            foreach (JobPickModel job in jobList)
            {
                NAPI.TextLabel.CreateTextLabel("/" + Messages.COM_JOB, job.position, 10.0f, 0.5f, 4, new Color(255, 255, 153), false, 0);
                NAPI.TextLabel.CreateTextLabel(Messages.GEN_JOB_HELP, new Vector3(job.position.X, job.position.Y, job.position.Z - 0.1f), 10.0f, 0.5f, 4, new Color(0, 0, 0), false, 0);
            }
        }

        [Command(Messages.COM_JOB, Messages.GEN_JOB_COMMAND)]
        public void JobCommand(Client player, string action)
        {
            int faction = player.GetData(EntityData.PLAYER_FACTION);
            int job = player.GetData(EntityData.PLAYER_JOB);

            switch (action.ToLower())
            {
                case Messages.ARG_INFO:
                    foreach (JobPickModel jobPick in jobList)
                    {
                        if (player.Position.DistanceTo(jobPick.position) < 1.5f)
                        {
                            player.SendChatMessage(Constants.COLOR_INFO + jobPick.description);
                            break;
                        }
                    }
                    break;
                case Messages.ARG_ACCEPT:
                    if (faction > 0 && faction < Constants.LAST_STATE_FACTION)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_JOB_STATE_FACTION);
                    }
                    else if (job > 0)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_HAS_JOB);
                    }
                    else
                    {
                        foreach (JobPickModel jobPick in jobList)
                        {
                            if (player.Position.DistanceTo(jobPick.position) < 1.5f)
                            {
                                player.SetData(EntityData.PLAYER_JOB, jobPick.job);
                                player.SetData(EntityData.PLAYER_EMPLOYEE_COOLDOWN, 5);
                                player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_JOB_ACCEPTED);
                                break;
                            }
                        }
                    }
                    break;
                case Messages.ARG_LEAVE:
                    // Get the hours spent in the current job
                    int employeeCooldown = player.GetData(EntityData.PLAYER_EMPLOYEE_COOLDOWN);

                    if (employeeCooldown > 0)
                    {
                        string message = string.Format(Messages.ERR_EMPLOYEE_COOLDOWN, employeeCooldown);
                        player.SendChatMessage(Constants.COLOR_ERROR + message);
                    }
                    else if (player.GetData(EntityData.PLAYER_JOB_RESTRICTION) > 0)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_JOB_RESTRICTION);
                    }
                    else if (job == 0)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NO_JOB);
                    }
                    else
                    {
                        player.SetData(EntityData.PLAYER_JOB, 0);
                        player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_JOB_LEFT);
                    }
                    break;
                default:
                    player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_JOB_COMMAND);
                    break;
            }
        }

        [Command(Messages.COM_DUTY)]
        public void DutyCommand(Client player)
        {
            // We get the sex, job and faction from the player
            int playerSex = player.GetData(EntityData.PLAYER_SEX);
            int playerJob = player.GetData(EntityData.PLAYER_JOB);
            int playerFaction = player.GetData(EntityData.PLAYER_FACTION);

            if (player.GetData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_IS_DEAD);
            }
            else if (playerJob == 0 && playerFaction == 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NO_JOB);
            }
            else if (player.GetData(EntityData.PLAYER_ON_DUTY) == 1)
            {
                // Populate player's clothes
                Customization.ApplyPlayerClothes(player);

                // We set the player off duty
                player.SetData(EntityData.PLAYER_ON_DUTY, 0);

                // Notification sent to the player
                player.SendNotification(Messages.INF_PLAYER_FREE_TIME);
            }
            else
            {
                // Dress the player with the uniform
                foreach (UniformModel uniform in Constants.UNIFORM_LIST)
                {
                    if (uniform.type == 0 && uniform.factionJob == playerFaction && playerSex == uniform.characterSex)
                    {
                        player.SetClothes(uniform.uniformSlot, uniform.uniformDrawable, uniform.uniformTexture);
                    }
                    else if (uniform.type == 1 && uniform.factionJob == playerJob && playerSex == uniform.characterSex)
                    {
                        player.SetClothes(uniform.uniformSlot, uniform.uniformDrawable, uniform.uniformTexture);
                    }
                }

                // We set the player on duty
                player.SetData(EntityData.PLAYER_ON_DUTY, 1);

                // Notification sent to the player
                player.SendNotification(Messages.INF_PLAYER_ON_DUTY);
            }
        }
    }
}

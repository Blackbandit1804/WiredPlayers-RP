using GTANetworkAPI;
using WiredPlayers.database;
using WiredPlayers.globals;
using WiredPlayers.model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace WiredPlayers.character
{
    public class Telephone : Script
    {
        public static List<ContactModel> contactList;

        private ContactModel GetContactFromId(int contactId)
        {
            ContactModel contact = null;
            foreach (ContactModel contactModel in contactList)
            {
                if (contactModel.id == contactId)
                {
                    contact = contactModel;
                    break;
                }
            }
            return contact;
        }

        private int GetNumerFromContactName(string contactName, int playerPhone)
        {
            int targetPhone = 0;
            foreach (ContactModel contact in contactList)
            {
                if (contact.owner == playerPhone && contact.contactName == contactName)
                {
                    targetPhone = contact.contactNumber;
                    break;
                }
            }
            return targetPhone;
        }

        private List<ContactModel> GetTelephoneContactList(int number)
        {
            List<ContactModel> contacts = new List<ContactModel>();
            foreach (ContactModel contact in contactList)
            {
                if (contact.owner == number)
                {
                    contacts.Add(contact);
                }
            }
            return contacts;
        }

        private string GetContactInTelephone(int phone, int number)
        {
            string contactName = string.Empty;
            foreach (ContactModel contact in contactList)
            {
                if (contact.owner == phone && contact.contactNumber == number)
                {
                    contactName = contact.contactName;
                    break;
                }
            }
            return contactName;
        }

        [RemoteEvent("addNewContact")]
        public void AddNewContactEvent(Client player, int contactNumber, string contactName)
        {
            // Create the model for the new contact
            ContactModel contact = new ContactModel();
            contact.owner = player.GetData(EntityData.PLAYER_PHONE);
            contact.contactNumber = contactNumber;
            contact.contactName = contactName;

            Task.Factory.StartNew(() =>
            {
                // Add contact to database
                contact.id = Database.AddNewContact(contact);
                contactList.Add(contact);
            });
            
            string actionMessage = string.Format(Messages.INF_CONTACT_CREATED, contactName, contactNumber);
            player.SendChatMessage(Constants.COLOR_INFO + actionMessage);
        }

        [RemoteEvent("modifyContact")]
        public void ModifyContactEvent(Client player, int contactIndex, int contactNumber, string contactName)
        {
            // Modify contact data
            ContactModel contact = GetContactFromId(contactIndex);
            contact.contactNumber = contactNumber;
            contact.contactName = contactName;

            Task.Factory.StartNew(() =>
            {
                // Modify the contact's data
                Database.ModifyContact(contact);
            });
            
            player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_CONTACT_MODIFIED);
        }

        [RemoteEvent("deleteContact")]
        public void DeleteContactEvent(Client player, int contactIndex)
        {
            ContactModel contact = GetContactFromId(contactIndex);
            string contactName = contact.contactName;
            int contactNumber = contact.contactNumber;

            Task.Factory.StartNew(() =>
            {
                // Delete the contact
                Database.DeleteContact(contactIndex);
                contactList.Remove(contact);
            });
            
            string actionMessage = string.Format(Messages.INF_CONTACT_DELETED, contactName, contactNumber);
            player.SendChatMessage(Constants.COLOR_INFO + actionMessage);
        }

        [RemoteEvent("sendPhoneMessage")]
        public void SendPhoneMessageEvent(Client player, int contactIndex, string textMessage)
        {
            ContactModel contact = GetContactFromId(contactIndex);
            
            foreach (Client target in NAPI.Pools.GetAllPlayers())
            {
                if (target.GetData(EntityData.PLAYER_PHONE) == contact.contactNumber)
                {
                    // Check player's number
                    int phone = target.GetData(EntityData.PLAYER_PHONE);
                    string contactName = GetContactInTelephone(phone, contact.contactNumber);

                    if (contactName.Length == 0)
                    {
                        contactName = contact.contactNumber.ToString();
                    }
                    
                    string secondMessage = string.Empty;

                    if (textMessage.Length > Constants.CHAT_LENGTH)
                    {
                        // We need to lines to print the message
                        secondMessage = textMessage.Substring(Constants.CHAT_LENGTH, textMessage.Length - Constants.CHAT_LENGTH);
                        textMessage = textMessage.Remove(Constants.CHAT_LENGTH, secondMessage.Length);
                    }

                    // Send the message to the target
                   target.SendChatMessage(secondMessage.Length > 0 ? Constants.COLOR_INFO + "[" + Messages.GEN_SMS_FROM + contactName + "] " + textMessage + "..." : Constants.COLOR_INFO + "[" + Messages.GEN_SMS_FROM + contactName + "] " + textMessage);
                    if (secondMessage.Length > 0)
                    {
                       target.SendChatMessage(Constants.COLOR_INFO + secondMessage);
                    }
                    
                    player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_SMS_SENT);

                    Task.Factory.StartNew(() =>
                    {
                        // Add the SMS to the database
                        Database.AddSMSLog(phone, contact.contactNumber, textMessage);
                    });

                    return;
                }
            }

            // There's no player matching the contact
            player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_PHONE_DISCONNECTED);
        }

        [Command(Messages.COM_CALL, Messages.GEN_PHONE_CALL_COMMAND, GreedyArg = true)]
        public void CallCommand(Client player, String called)
        {
            player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_PHONE_CALL_COMMAND);
            if (called.Length == 0)
            {
                player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_PHONE_CALL_COMMAND);
                return;
            }

            if (player.HasData(EntityData.PLAYER_PHONE_TALKING) || player.HasData(EntityData.PLAYER_CALLING) == true)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_ALREADY_PHONE_TALKING);
            }
            else
            {
                ItemModel item = Globals.GetItemInEntity(player.GetData(EntityData.PLAYER_SQL_ID), Constants.ITEM_ENTITY_RIGHT_HAND);
                if (item != null && item.hash == Constants.ITEM_HASH_TELEPHONE)
                {
                    int peopleOnline = 0;

                    if (int.TryParse(called, out int number) == true)
                    {
                        switch (number)
                        {
                            case Constants.NUMBER_POLICE:
                                foreach (Client target in NAPI.Pools.GetAllPlayers())
                                {
                                    if (target.HasData(EntityData.PLAYER_PLAYING) && target.GetData(EntityData.PLAYER_FACTION) == Constants.FACTION_POLICE)
                                    {
                                       target.SendChatMessage(Constants.COLOR_INFO + Messages.INF_CENTRAL_CALL);
                                        peopleOnline++;
                                    }
                                }
                                
                                if (peopleOnline > 0)
                                {
                                    player.SetData(EntityData.PLAYER_CALLING, Constants.FACTION_POLICE);
                                    
                                    string playerMessage = string.Format(Messages.INF_CALLING, Constants.NUMBER_POLICE);
                                    player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_LINE_OCCUPIED);
                                }
                                break;
                            case Constants.NUMBER_EMERGENCY:
                                foreach (Client target in NAPI.Pools.GetAllPlayers())
                                {
                                    if (target.HasData(EntityData.PLAYER_PLAYING) && target.GetData(EntityData.PLAYER_FACTION) == Constants.FACTION_EMERGENCY)
                                    {
                                       target.SendChatMessage(Constants.COLOR_INFO + Messages.INF_CENTRAL_CALL);
                                        peopleOnline++;
                                    }
                                }
                                
                                if (peopleOnline > 0)
                                {
                                    player.SetData(EntityData.PLAYER_CALLING, Constants.FACTION_EMERGENCY);
                                    
                                    string playerMessage = string.Format(Messages.INF_CALLING, Constants.NUMBER_EMERGENCY);
                                    player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_LINE_OCCUPIED);
                                }
                                break;
                            case Constants.NUMBER_NEWS:
                                foreach (Client target in NAPI.Pools.GetAllPlayers())
                                {
                                    if (target.HasData(EntityData.PLAYER_PLAYING) && target.GetData(EntityData.PLAYER_FACTION) == Constants.FACTION_NEWS)
                                    {
                                       target.SendChatMessage(Constants.COLOR_INFO + Messages.INF_CENTRAL_CALL);
                                        peopleOnline++;
                                    }
                                }
                                
                                if (peopleOnline > 0)
                                {
                                    player.SetData(EntityData.PLAYER_CALLING, Constants.FACTION_NEWS);
                                    
                                    string playerMessage = string.Format(Messages.INF_CALLING, Constants.NUMBER_NEWS);
                                    player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_LINE_OCCUPIED);
                                }
                                break;
                            case Constants.NUMBER_TAXI:
                                foreach (Client target in NAPI.Pools.GetAllPlayers())
                                {
                                    if (target.HasData(EntityData.PLAYER_PLAYING) && target.GetData(EntityData.PLAYER_FACTION) == Constants.FACTION_TAXI_DRIVER)
                                    {
                                       target.SendChatMessage(Constants.COLOR_INFO + Messages.INF_CENTRAL_CALL);
                                        peopleOnline++;
                                    }
                                }
                                
                                if (peopleOnline > 0)
                                {
                                    player.SetData(EntityData.PLAYER_CALLING, Constants.FACTION_TAXI_DRIVER);
                                    
                                    string playerMessage = string.Format(Messages.INF_CALLING, Constants.NUMBER_TAXI);
                                    player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_LINE_OCCUPIED);
                                }
                                break;
                            case Constants.NUMBER_FASTFOOD:
                                foreach (Client target in NAPI.Pools.GetAllPlayers())
                                {
                                    if (target.HasData(EntityData.PLAYER_PLAYING) && target.GetData(EntityData.PLAYER_JOB) == Constants.JOB_FASTFOOD)
                                    {
                                       target.SendChatMessage(Constants.COLOR_INFO + Messages.INF_CENTRAL_CALL);
                                        peopleOnline++;
                                    }
                                }
                                
                                if (peopleOnline > 0)
                                {
                                    player.SetData(EntityData.PLAYER_CALLING, Constants.JOB_FASTFOOD + 100);
                                    
                                    string playerMessage = string.Format(Messages.INF_CALLING, Constants.NUMBER_FASTFOOD);
                                    player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_LINE_OCCUPIED);
                                }
                                break;
                            case Constants.NUMBER_MECHANIC:
                                foreach (Client target in NAPI.Pools.GetAllPlayers())
                                {
                                    if (target.HasData(EntityData.PLAYER_PLAYING) && target.GetData(EntityData.PLAYER_JOB) == Constants.JOB_MECHANIC)
                                    {
                                       target.SendChatMessage(Constants.COLOR_INFO + Messages.INF_CENTRAL_CALL);
                                        peopleOnline++;
                                    }
                                }
                                
                                if (peopleOnline > 0)
                                {
                                    player.SetData(EntityData.PLAYER_CALLING, Constants.JOB_MECHANIC + 100);
                                    
                                    string playerMessage = string.Format(Messages.INF_CALLING, Constants.NUMBER_MECHANIC);
                                    player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_LINE_OCCUPIED);
                                }
                                break;
                            default:
                                if (number > 0)
                                {
                                    foreach (Client target in NAPI.Pools.GetAllPlayers())
                                    {
                                        if (target.GetData(EntityData.PLAYER_PHONE) == number)
                                        {
                                            int playerPhone = player.GetData(EntityData.PLAYER_PHONE);

                                            // Check if the player has the number as contact
                                            int phone = target.GetData(EntityData.PLAYER_PHONE);
                                            string contact = GetContactInTelephone(phone, playerPhone);

                                            if (contact.Length == 0)
                                            {
                                                contact = playerPhone.ToString();
                                            }

                                            player.SetData(EntityData.PLAYER_CALLING, target);

                                            // Check if the player calling is a contact into target's contact list
                                            string playerMessage = string.Format(Messages.INF_CALLING, number);
                                            string targetMessage = string.Format(Messages.INF_CALL_FROM, contact.Length > 0 ? contact : contact.ToString());
                                            
                                            player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                                           target.SendChatMessage(Constants.COLOR_INFO + targetMessage);

                                            return;
                                        }
                                    }
                                }

                                // The phone number doesn't exist
                                player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_PHONE_DISCONNECTED);
                                break;
                        }
                    }
                    else
                    {
                        // Call a contact
                        int playerPhone = player.GetData(EntityData.PLAYER_PHONE);
                        int targetPhone = GetNumerFromContactName(called, playerPhone);
                        
                        if (targetPhone > 0)
                        {
                            foreach (Client target in NAPI.Pools.GetAllPlayers())
                            {
                                if (target.GetData(EntityData.PLAYER_PHONE) == targetPhone)
                                {
                                    if (target.HasData(EntityData.PLAYER_CALLING) || target.HasData(EntityData.PLAYER_PHONE_TALKING) || player.HasData(EntityData.PLAYER_PLAYING) == false)
                                    {
                                        player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_PHONE_DISCONNECTED);
                                    }
                                    else
                                    {
                                        player.SetData(EntityData.PLAYER_CALLING, target);

                                        // Check if the player is in target's contact list
                                        string contact = GetContactInTelephone(target.GetData(EntityData.PLAYER_PHONE), playerPhone);

                                        string playerMessage = string.Format(Messages.INF_CALLING, called);
                                        string targetMessage = string.Format(Messages.INF_CALL_FROM, contact.Length > 0 ? contact : playerPhone.ToString());
                                        player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                                       target.SendChatMessage(Constants.COLOR_INFO + Messages.INF_INCOMING_CALL);
                                    }
                                    return;
                                }
                            }
                        }

                        // The contact player isn't online
                        player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_PHONE_DISCONNECTED);
                    }
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_TELEPHONE_HAND);
                }
            }
        }

        [Command(Messages.COM_ANSWER)]
        public void AnswerCommand(Client player)
        {
            if (player.HasData(EntityData.PLAYER_CALLING) || player.HasData(EntityData.PLAYER_PHONE_TALKING) == true)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_ALREADY_PHONE_TALKING);
            }
            else
            {
                foreach (Client target in NAPI.Pools.GetAllPlayers())
                {
                    // Check if the target player is calling somebody
                    if (target.HasData(EntityData.PLAYER_CALLING) == true)
                    {
                        if (target.GetData(EntityData.PLAYER_CALLING) is int)
                        {
                            int factionJob = target.GetData(EntityData.PLAYER_CALLING);
                            int faction = player.GetData(EntityData.PLAYER_FACTION);
                            int job = player.GetData(EntityData.PLAYER_JOB);

                            if (factionJob == faction || factionJob == job + 100)
                            {
                                // Link both players in the same call
                                target.ResetData(EntityData.PLAYER_CALLING);
                                player.SetData(EntityData.PLAYER_PHONE_TALKING, target);
                                target.SetData(EntityData.PLAYER_PHONE_TALKING, player);
                                
                                player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_CALL_RECEIVED);
                                target.SendChatMessage(Constants.COLOR_INFO + Messages.INF_CALL_TAKEN);

                                // Store call starting time
                                target.SetData(EntityData.PLAYER_PHONE_CALL_STARTED, Globals.GetTotalSeconds());
                                return;
                            }
                        }
                        else if (target.GetData(EntityData.PLAYER_CALLING) == player)
                        {
                            // Link both players in the same call
                            target.ResetData(EntityData.PLAYER_CALLING);
                            player.SetData(EntityData.PLAYER_PHONE_TALKING, target);
                            target.SetData(EntityData.PLAYER_PHONE_TALKING, player);
                            
                            player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_CALL_RECEIVED);
                           target.SendChatMessage(Constants.COLOR_INFO + Messages.INF_CALL_TAKEN);

                            // Store call starting time
                            target.SetData(EntityData.PLAYER_PHONE_CALL_STARTED, Globals.GetTotalSeconds());
                            return;
                        }
                    }
                }

                // Nobody's calling the player
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_PLAYER_NOT_CALLED);
            }
        }

        [Command(Messages.COM_HANG)]
        public void HangCommand(Client player)
        {
            if (player.HasData(EntityData.PLAYER_CALLING) == true)
            {
                // Hang up the call
                player.ResetData(EntityData.PLAYER_CALLING);
            }
            else if (player.HasData(EntityData.PLAYER_PHONE_TALKING) == true)
            {
                // Get the player he's talking with
                int elapsed = 0;
                Client target = player.GetData(EntityData.PLAYER_PHONE_TALKING);
                int playerPhone = player.GetData(EntityData.PLAYER_PHONE);
                int targetPhone = target.GetData(EntityData.PLAYER_PHONE);

                // Get phone call time
                if (player.HasData(EntityData.PLAYER_PHONE_CALL_STARTED) == true)
                {
                    elapsed = Globals.GetTotalSeconds() - player.GetData(EntityData.PLAYER_PHONE_CALL_STARTED);

                    Task.Factory.StartNew(() =>
                    {
                        // Update the elapsed time into the database
                        Database.AddCallLog(playerPhone, targetPhone, elapsed);
                    });
                }
                else
                {
                    elapsed = Globals.GetTotalSeconds() - target.GetData(EntityData.PLAYER_PHONE_CALL_STARTED);

                    Task.Factory.StartNew(() =>
                    {
                        // Update the elapsed time into the database
                        Database.AddCallLog(targetPhone, playerPhone, elapsed);
                    });
                }

                // Hang up the call for both players
                player.ResetData(EntityData.PLAYER_PHONE_TALKING);
                target.ResetData(EntityData.PLAYER_PHONE_TALKING);
                player.ResetData(EntityData.PLAYER_PHONE_CALL_STARTED);

                player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_FINISHED_CALL);
                target.SendChatMessage(Constants.COLOR_INFO + Messages.INF_FINISHED_CALL);
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NOT_PHONE_TALKING);
            }
        }

        [Command(Messages.COM_SMS, Messages.GEN_SMS_COMMAND, GreedyArg = true)]
        public void SmsCommand(Client player, int number, string message)
        {
            ItemModel item = Globals.GetItemInEntity(player.GetData(EntityData.PLAYER_SQL_ID), Constants.ITEM_ENTITY_RIGHT_HAND);
            if (item != null && item.hash == Constants.ITEM_HASH_TELEPHONE)
            {
                foreach (Client target in NAPI.Pools.GetAllPlayers())
                {
                    if (number > 0 && target.GetData(EntityData.PLAYER_PHONE) == number)
                    {
                        int playerPhone = player.GetData(EntityData.PLAYER_PHONE);

                        // Check if the player's in the contact list
                        int phone = target.GetData(EntityData.PLAYER_PHONE);
                        string contact = GetContactInTelephone(phone, playerPhone);

                        if (contact.Length == 0)
                        {
                            contact = playerPhone.ToString();
                        }
                        
                        string secondMessage = string.Empty;

                        if (message.Length > Constants.CHAT_LENGTH)
                        {
                            // We need two lines to print the full message
                            secondMessage = message.Substring(Constants.CHAT_LENGTH, message.Length - Constants.CHAT_LENGTH);
                            message = message.Remove(Constants.CHAT_LENGTH, secondMessage.Length);
                        }
                        
                       target.SendChatMessage(secondMessage.Length > 0 ? Constants.COLOR_INFO + "[" + Messages.GEN_SMS_FROM + playerPhone + "] " + message + "..." : Constants.COLOR_INFO + "[" + Messages.GEN_SMS_FROM + playerPhone + "] " + message);
                        if (secondMessage.Length > 0)
                        {
                           target.SendChatMessage(Constants.COLOR_INFO + secondMessage);
                        }

                        foreach (Client targetPlayer in NAPI.Pools.GetAllPlayers())
                        {
                            if (targetPlayer.Position.DistanceTo(player.Position) < 20.0f)
                            {
                                string nearMessage = string.Format(Messages.INF_PLAYER_TEXTING, player.Name);
                                targetPlayer.SendChatMessage(Constants.COLOR_CHAT_ME + nearMessage);
                            }
                        }

                        Task.Factory.StartNew(() =>
                        {
                            // Add the SMS into the database
                            Database.AddSMSLog(playerPhone, number, message);
                        });

                        return;
                    }
                }

                // The phone doesn't exist
                player.SendChatMessage(Constants.COLOR_INFO + Messages.INF_PHONE_DISCONNECTED);
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_TELEPHONE_HAND);
            }
        }

        [Command(Messages.COM_CONTACTS, Messages.GEN_CONTACTS_COMMAND)]
        public void AgendaCommand(Client player, string action)
        {
            ItemModel item = Globals.GetItemInEntity(player.GetData(EntityData.PLAYER_SQL_ID), Constants.ITEM_ENTITY_RIGHT_HAND);
            if (item != null && item.hash == Constants.ITEM_HASH_TELEPHONE)
            {
                // Get the contact list
                int phoneNumber = player.GetData(EntityData.PLAYER_PHONE);
                List<ContactModel> contacts = GetTelephoneContactList(phoneNumber);

                switch (action.ToLower())
                {
                    case Messages.ARG_NUMBER:
                        string message = string.Format(Messages.INF_PHONE_NUMBER, phoneNumber);
                        player.SendChatMessage(Constants.COLOR_INFO + message);
                        break;
                    case Messages.ARG_VIEW:
                        if (contacts.Count > 0)
                        {
                            player.TriggerEvent("showPhoneContacts", NAPI.Util.ToJson(contacts), Constants.ACTION_LOAD);
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_CONTACT_LIST_EMPTY);
                        }
                        break;
                    case Messages.ARG_ADD:
                        player.TriggerEvent("addContactWindow", Constants.ACTION_ADD);
                        break;
                    case Messages.ARG_MODIFY:
                        if (contacts.Count > 0)
                        {
                            player.TriggerEvent("showPhoneContacts", NAPI.Util.ToJson(contacts), Constants.ACTION_RENAME);
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_CONTACT_LIST_EMPTY);
                        }
                        break;
                    case Messages.ARG_REMOVE:
                        if (contacts.Count > 0)
                        {
                            player.TriggerEvent("showPhoneContacts", NAPI.Util.ToJson(contacts), Constants.ACTION_DELETE);
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_CONTACT_LIST_EMPTY);
                        }
                        break;
                    case Messages.ARG_SMS:
                        if (contacts.Count > 0)
                        {
                            player.TriggerEvent("showPhoneContacts", NAPI.Util.ToJson(contacts), Constants.ACTION_SMS);
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_CONTACT_LIST_EMPTY);
                        }
                        break;
                    default:
                        player.SendChatMessage(Constants.COLOR_HELP + Messages.GEN_CONTACTS_COMMAND);
                        break;
                }
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Messages.ERR_NO_TELEPHONE_HAND);
            }
        }
    }
}
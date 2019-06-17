using GTANetworkAPI;
using WiredPlayers.model;
using System.Collections.Generic;
using System;

namespace WiredPlayers.globals
{
    public class Constants
    {
        // Gamemode version
        public const string GM_VERSION = "v1.1.0";

        public const int ENVIRONMENT_KILL = 65535;
        public const int ITEMS_PER_INVENTORY_PAGE = 16;
        public const decimal ITEMS_ROBBED_PER_TIME = 1.5m;
        public const int MAX_GARBAGE_ROUTES = 4;
        public const int TOTAL_COLOR_ELEMENTS = 3;
        public const int MAX_BANK_OPERATIONS = 25;
        public const int MAX_LICENSE_QUESTIONS = 3;
        public const int MAX_DRIVING_VEHICLE = 100;
        public const int REDUCTION_PER_KMS = 125;
        public const int MAX_THEFTS_IN_ROW = 4;
        public const int MAX_WEAPON_SPAWNS = 1;
        public const int MAX_CRATES_SPAWN = 12;
        public const int MAX_WEAPON_CHANCE = 1235;
        public const int MAX_AMMO_CHANCE = 500;
        public const float GAS_CAN_LITRES = 10.0f;
        public const float LEVEL_MULTIPLIER = 3.25f;
        public const int PAID_PER_LEVEL = 30;
        public const float HOUSE_SALE_STATE = 0.7f;
        public const int MAX_HEAD_OVERLAYS = 11;

        // Sex
        public const int SEX_NONE = -1;
        public const int SEX_MALE = 0;
        public const int SEX_FEMALE = 1;

        // Chat
        public const int CHAT_LENGTH = 85;
        public const int CHAT_RANGES = 5;

        // Jail types
        public const int JAIL_TYPE_IC = 1;
        public const int JAIL_TYPE_OOC = 2;

        // Administrative ranks
        public const int STAFF_NONE = 0;
        public const int STAFF_SUPPORT = 1;
        public const int STAFF_GAME_MASTER = 2;
        public const int STAFF_S_GAME_MASTER = 3;
        public const int STAFF_ADMIN = 4;

        // Actions
        public const int ACTION_LOAD = 0;
        public const int ACTION_SAVE = 1;
        public const int ACTION_RENAME = 2;
        public const int ACTION_DELETE = 3;
        public const int ACTION_ADD = 4;
        public const int ACTION_SMS = 5;

        // Business types
        public const int BUSINESS_TYPE_NONE = -1;
        public const int BUSINESS_TYPE_24_7 = 1;
        public const int BUSINESS_TYPE_ELECTRONICS = 2;
        public const int BUSINESS_TYPE_HARDWARE = 3;
        public const int BUSINESS_TYPE_CLOTHES = 4;
        public const int BUSINESS_TYPE_BAR = 5;
        public const int BUSINESS_TYPE_DISCO = 6;
        public const int BUSINESS_TYPE_AMMUNATION = 7;
        public const int BUSINESS_TYPE_WAREHOUSE = 8;
        public const int BUSINESS_TYPE_JEWELRY = 9;
        public const int BUSINESS_TYPE_PRIVATE_OFFICE = 10;
        public const int BUSINESS_TYPE_CLUBHOUSE = 11;
        public const int BUSINESS_TYPE_GAS_STATION = 12;
        public const int BUSINESS_TYPE_SLAUGHTERHOUSE = 13;
        public const int BUSINESS_TYPE_BARBER_SHOP = 14;
        public const int BUSINESS_TYPE_FACTORY = 15;
        public const int BUSINESS_TYPE_TORTURE_ROOM = 16;
        public const int BUSINESS_TYPE_GARAGE_LOW_END = 17;
        public const int BUSINESS_TYPE_WAREHOUSE_MEDIUM = 18;
        public const int BUSINESS_TYPE_SOCIAL_CLUB = 19;
        public const int BUSINESS_TYPE_MECHANIC = 20;
        public const int BUSINESS_TYPE_TATTOO_SHOP = 21;
        public const int BUSINESS_TYPE_BENNYS_WHORKSHOP = 22;
        public const int BUSINESS_TYPE_VANILLA = 23;
        public const int BUSINESS_TYPE_FISHING = 24;

        // Phone numbers
        public const int NUMBER_POLICE = 091;
        public const int NUMBER_EMERGENCY = 112;
        public const int NUMBER_NEWS = 114;
        public const int NUMBER_FASTFOOD = 115;
        public const int NUMBER_MECHANIC = 116;
        public const int NUMBER_TAXI = 555;

        // Parking types
        public const int PARKING_TYPE_PUBLIC = 0;
        public const int PARKING_TYPE_GARAGE = 1;
        public const int PARKING_TYPE_SCRAPYARD = 2;
        public const int PARKING_TYPE_DEPOSIT = 3;

        // Clothes bodyparts
        public const int CLOTHES_MASK = 1;
        public const int CLOTHES_TORSO = 3;
        public const int CLOTHES_LEGS = 4;
        public const int CLOTHES_BAGS = 5;
        public const int CLOTHES_FEET = 6;
        public const int CLOTHES_ACCESSORIES = 7;
        public const int CLOTHES_UNDERSHIRT = 8;
        public const int CLOTHES_TOPS = 11;

        // Tattoo zones
        public const int TATTOO_ZONE_TORSO = 0;
        public const int TATTOO_ZONE_HEAD = 1;
        public const int TATTOO_ZONE_LEFT_ARM = 2;
        public const int TATTOO_ZONE_RIGHT_ARM = 3;
        public const int TATTOO_ZONE_LEFT_LEG = 4;
        public const int TATTOO_ZONE_RIGHT_LEG = 5;

        // Accessory types
        public const int ACCESSORY_HATS = 0;
        public const int ACCESSORY_GLASSES = 1;
        public const int ACCESSORY_EARS = 2;

        // Vehicle components
        public const int VEHICLE_MOD_SPOILER = 0;
        public const int VEHICLE_MOD_FRONT_BUMPER = 1;
        public const int VEHICLE_MOD_REAR_BUMPER = 2;
        public const int VEHICLE_MOD_SIDE_SKIRT = 3;
        public const int VEHICLE_MOD_EXHAUST = 4;
        public const int VEHICLE_MOD_FRAME = 5;
        public const int VEHICLE_MOD_GRILLE = 6;
        public const int VEHICLE_MOD_HOOD = 7;
        public const int VEHICLE_MOD_FENDER = 8;
        public const int VEHICLE_MOD_RIGHT_FENDER = 9;
        public const int VEHICLE_MOD_ROOF = 10;
        public const int VEHICLE_MOD_ENGINE = 11;
        public const int VEHICLE_MOD_BRAKES = 12;
        public const int VEHICLE_MOD_TRANSMISSION = 13;
        public const int VEHICLE_MOD_HORN = 14;
        public const int VEHICLE_MOD_SUSPENSION = 15;
        public const int VEHICLE_MOD_ARMOR = 16;
        public const int VEHICLE_MOD_XENON = 22;
        public const int VEHICLE_MOD_FRONT_WHEELS = 23;
        public const int VEHICLE_MOD_BACK_WHEELS = 24;
        public const int VEHICLE_MOD_PLATE_HOLDERS = 25;
        public const int VEHICLE_MOD_TRIM_DESIGN = 27;
        public const int VEHICLE_MOD_ORNAMIENTS = 28;
        public const int VEHICLE_MOD_DIAL_DESIGN = 30;
        public const int VEHICLE_MOD_STEERING_WHEEL = 33;
        public const int VEHICLE_MOD_SHIFTER_LEAVERS = 34;
        public const int VEHICLE_MOD_PLAQUES = 35;
        public const int VEHICLE_MOD_HYDRAULICS = 38;
        public const int VEHICLE_MOD_LIVERY = 48;

        // Inventory targets
        public const int INVENTORY_TARGET_SELF = 0;
        public const int INVENTORY_TARGET_PLAYER = 1;
        public const int INVENTORY_TARGET_HOUSE = 2;
        public const int INVENTORY_TARGET_VEHICLE_TRUNK = 3;
        public const int INVENTORY_TARGET_VEHICLE_PLAYER = 4;

        // Item types
        public const int ITEM_TYPE_CONSUMABLE = 0;
        public const int ITEM_TYPE_EQUIPABLE = 1;
        public const int ITEM_TYPE_OPENABLE = 2;
        public const int ITEM_TYPE_WEAPON = 3;
        public const int ITEM_TYPE_AMMUNITION = 4;
        public const int ITEM_TYPE_MISC = 5;

        // Amount of items when container opened
        public const int ITEM_OPEN_BEER_AMOUNT = 6;

        // 24-7 items
        public const string ITEM_HASH_FRIES = "1443311452";
        public const string ITEM_HASH_HOTDOG = "2565741261";
        public const string ITEM_HASH_CHOCOLATE_BAR = "921283475";
        public const string ITEM_HASH_BURGER = "2240524752";
        public const string ITEM_HASH_SANDWICH = "3602873787";
        public const string ITEM_HASH_CANDY = "3310697493";

        public const string ITEM_HASH_CUP_JUICE = "3638960837";
        public const string ITEM_HASH_ENERGY_DRINK = "582043502";
        public const string ITEM_HASH_BOTTLE_WATER = "746336278";
        public const string ITEM_HASH_CUP_COFFEE = "3696781377";
        public const string ITEM_HASH_CAN_COLA = "1020618269";

        public const string ITEM_HASH_CUP_WINE = "2998419875";
        public const string ITEM_HASH_CUP_CHAMPANGE = "600913159";
        public const string ITEM_HASH_BOTTLE_BEER_PISSWASSER = "4016900153";
        public const string ITEM_HASH_BOTTLE_BEER_AM = "1350970027";
        public const string ITEM_HASH_PACK_BEER_AM = "4241316616";
        public const string ITEM_HASH_BOTTLE_COGNAC = "1404018125";
        public const string ITEM_HASH_BOTTLE_CAVA = "3846720762";

        public const string ITEM_HASH_CIGARRETES_PACK_OPEN = "1079465856";

        // Electronic items
        public const string ITEM_HASH_TELEPHONE = "2277609629";
        public const string ITEM_HASH_WALKIE = "1806057883";
        public const string ITEM_HASH_RADIO_CASSETTE = "1060029110";
        public const string ITEM_HASH_CAMERA = "680380202";

        // Ammunition items
        public const string ITEM_HASH_PISTOL_AMMO_CLIP = "PistolAmmo";
        public const string ITEM_HASH_MACHINEGUN_AMMO_CLIP = "SmgAmmo";
        public const string ITEM_HASH_ASSAULTRIFLE_AMMO_CLIP = "RifleAmmo";
        public const string ITEM_HASH_SNIPERRIFLE_AMMO_CLIP = "SniperAmmo";
        public const string ITEM_HASH_SHOTGUN_AMMO_CLIP = "ShotgunAmmo";

        // Stack of the guns
        public const Int32 STACK_PISTOL_CAPACITY = 32;
        public const Int32 STACK_MACHINEGUN_CAPACITY = 100;
        public const Int32 STACK_SHOTGUN_CAPACITY = 24;
        public const Int32 STACK_ASSAULTRIFLE_CAPACITY = 60;
        public const Int32 STACK_SNIPERRIFLE_CAPACITY = 8;

        // Miscelaneous items
        public const string ITEM_HASH_ID_CARD = "511938898";
        public const string ITEM_HASH_CUFFS = "1070220657";
        public const string ITEM_HASH_JERRYCAN = "1069395324";
        public const string ITEM_HASH_FISHING_ROD = "2384362703";
        public const string ITEM_HASH_STOLEN_OBJECTS = "Stolen";
        public const string ITEM_HASH_BUSINESS_PRODUCTS = "Products";
        public const string ITEM_HASH_BAIT = "Bait";
        public const string ITEM_HASH_FISH = "Fish";

        // Vehicle color types
        public const int VEHICLE_COLOR_TYPE_PREDEFINED = 0;
        public const int VEHICLE_COLOR_TYPE_CUSTOM = 1;
        
        // Vehicle types
        public const int VEHICLE_CLASS_COMPACTS = 0;
        public const int VEHICLE_CLASS_SEDANS = 1;
        public const int VEHICLE_CLASS_SUVS = 2;
        public const int VEHICLE_CLASS_COUPES = 3;
        public const int VEHICLE_CLASS_MUSCLE = 4;
        public const int VEHICLE_CLASS_SPORTS = 5;
        public const int VEHICLE_CLASS_CLASSICS = 6;
        public const int VEHICLE_CLASS_SUPER = 7;
        public const int VEHICLE_CLASS_MOTORCYCLES = 8;
        public const int VEHICLE_CLASS_OFFROAD = 9;
        public const int VEHICLE_CLASS_INDUSTRIAL = 10;
        public const int VEHICLE_CLASS_UTILITY = 11;
        public const int VEHICLE_CLASS_VANS = 12;
        public const int VEHICLE_CLASS_CYCLES = 13;
        public const int VEHICLE_CLASS_BOATS = 14;
        public const int VEHICLE_CLASS_HELICOPTERS = 15;
        public const int VEHICLE_CLASS_PLANES = 16;
        public const int VEHICLE_CLASS_SERVICE = 17;
        public const int VEHICLE_CLASS_EMERGENCY = 18;
        public const int VEHICLE_CLASS_MILITARY = 19;
        public const int VEHICLE_CLASS_COMMERCIAL = 20;
        public const int VEHICLE_CLASS_TRAINS = 21;

        // Tax percentage
        public const float TAXES_VEHICLE = 0.005f;
        public const float TAXES_HOUSE = 0.0015f;

        // Gargabe route money
        public const int MONEY_GARBAGE_ROUTE = 350;

        // Price in products
        public const int PRICE_VEHICLE_CHASSIS = 300;
        public const int PRICE_VEHICLE_DOORS = 60;
        public const int PRICE_VEHICLE_WINDOWS = 15;
        public const int PRICE_VEHICLE_TYRES = 10;
        public const int PRICE_BARBER_SHOP = 100;
        public const int PRICE_ANNOUNCEMENT = 500;
        public const int PRICE_DRIVING_THEORICAL = 200;
        public const int PRICE_DRIVING_PRACTICAL = 300;
        public const int PRICE_IDENTIFICATION = 500;
        public const int PRICE_MEDICAL_INSURANCE = 2000;
        public const int PRICE_TAXI_LICENSE = 5000;
        public const int PRICE_STOLEN = 20;
        public const int PRICE_PARKING_PUBLIC = 50;
        public const int PRICE_PARKING_DEPOSIT = 500;
        public const int PRICE_PIZZA = 20;
        public const int PRICE_HAMBURGER = 10;
        public const int PRICE_SANDWICH = 5;
        public const int PRICE_GAS = 1;
        public const int PRICE_FISH = 20;

        // Factions
        public const int FACTION_NONE = 0;
        public const int FACTION_POLICE = 1;
        public const int FACTION_EMERGENCY = 2;
        public const int FACTION_NEWS = 3;
        public const int FACTION_TOWNHALL = 4;
        public const int FACTION_DRIVING_SCHOOL = 5;
        public const int FACTION_TAXI_DRIVER = 6;
        public const int FACTION_ADMIN = 9;
        public const int LAST_STATE_FACTION = 10;
        public const int MAX_FACTION_VEHICLES = 100;

        // Jobs
        public const int JOB_NONE = 0;
        public const int JOB_FASTFOOD = 1;
        public const int JOB_THIEF = 2;
        public const int JOB_MECHANIC = 3;
        public const int JOB_GARBAGE = 4;
        public const int JOB_HOOKER = 5;
        public const int JOB_FISHERMAN = 6;
        public const int JOB_TAXI = 7;

        // Database stored items' place
        public const string ITEM_ENTITY_GROUND = "Ground";
        public const string ITEM_ENTITY_PLAYER = "Player";
        public const string ITEM_ENTITY_VEHICLE = "Vehicle";
        public const string ITEM_ENTITY_HOUSE = "House";
        public const string ITEM_ENTITY_WHEEL = "Wheel";
        public const string ITEM_ENTITY_LEFT_HAND = "Left hand";
        public const string ITEM_ENTITY_RIGHT_HAND = "Right hand";

        // Driving school's licenses
        public const int LICENSE_CAR = 0;
        public const int LICENSE_MOTORCYCLE = 1;
        public const int LICENSE_TAXI = 2;

        // Driving school exam type
        public const int CAR_DRIVING_THEORICAL = 1;
        public const int CAR_DRIVING_PRACTICE = 2;
        public const int MOTORCYCLE_DRIVING_THEORICAL = 3;
        public const int MOTORCYCLE_DRIVING_PRACTICE = 4;

        // Town hall formalities
        public const int TRAMITATE_IDENTIFICATION = 0;
        public const int TRAMITATE_MEDICAL_INSURANCE = 1;
        public const int TRAMITATE_TAXI_LICENSE = 2;
        public const int TRAMITATE_FINE_LIST = 3;

        // Bank operations
        public const int OPERATION_WITHDRAW = 1;
        public const int OPERATION_DEPOSIT = 2;
        public const int OPERATION_TRANSFER = 3;
        public const int OPERATION_BALANCE = 4;

        // House status
        public const int HOUSE_STATE_NONE = 0;
        public const int HOUSE_STATE_RENTABLE = 1;
        public const int HOUSE_STATE_BUYABLE = 2;

        // Police control's items
        public const int POLICE_DEPLOYABLE_CONE = 1245865676;
        public const int POLICE_DEPLOYABLE_BEACON = 93871477;
        public const int POLICE_DEPLOYABLE_BARRIER = -143315610;
        public const int POLICE_DEPLOYABLE_SPIKES = -874338148;

        // Chat message types
        public const int MESSAGE_TALK = 0;
        public const int MESSAGE_YELL = 1;
        public const int MESSAGE_WHISPER = 2;
        public const int MESSAGE_ME = 3;
        public const int MESSAGE_DO = 4;
        public const int MESSAGE_OOC = 5;
        public const int MESSAGE_SU_TRUE = 6;
        public const int MESSAGE_SU_FALSE = 7;
        public const int MESSAGE_NEWS = 8;
        public const int MESSAGE_PHONE = 9;
        public const int MESSAGE_DISCONNECT = 10;
        public const int MESSAGE_MEGAPHONE = 11;
        public const int MESSAGE_RADIO = 12;

        // Chat colors
        public const string COLOR_CHAT_CLOSE = "!{#E6E6E6}";
        public const string COLOR_CHAT_NEAR = "!{#C8C8C8}";
        public const string COLOR_CHAT_MEDIUM = "!{#AAAAAA}";
        public const string COLOR_CHAT_FAR = "!{#8C8C8C}";
        public const string COLOR_CHAT_LIMIT = "!{#6E6E6E}";
        public const string COLOR_CHAT_ME = "!{#C2A2DA}";
        public const string COLOR_CHAT_DO = "!{#0F9622}";
        public const string COLOR_CHAT_FACTION = "!{#27F7C8}";
        public const string COLOR_CHAT_PHONE = "!{#27F7C8}";
        public const string COLOR_OOC_CLOSE = "!{#4C9E9E}";
        public const string COLOR_OOC_NEAR = "!{#438C8C}";
        public const string COLOR_OOC_MEDIUM = "!{#2E8787}";
        public const string COLOR_OOC_FAR = "!{#187373}";
        public const string COLOR_OOC_LIMIT = "!{#0A5555}";
        public const string COLOR_ADMIN_INFO = "!{#00FCFF}";
        public const string COLOR_ADMIN_NEWS = "!{#F93131}";
        public const string COLOR_ADMIN_MP = "!{#F93131}";
        public const string COLOR_SUCCESS = "!{#33B517}";
        public const string COLOR_ERROR = "!{#A80707}";
        public const string COLOR_INFO = "!{#FDFE8B}";
        public const string COLOR_HELP = "!{#FFFFFF}";
        public const string COLOR_SU_POSITIVE = "!{#E3E47D}";
        public const string COLOR_RADIO = "!{#1598C4}";
        public const string COLOR_RADIO_POLICE = "!{#4169E1}";
        public const string COLOR_RADIO_EMERGENCY = "!{#FF9F0F}";
        public const string COLOR_NEWS = "!{#805CC9}";

        // Gargabe collector's routes
        public const int NORTH_ROUTE = 0;
        public const int EAST_ROUTE = 1;
        public const int SOUTH_ROUTE = 2;
        public const int WEST_ROUTE = 3;

        // Hooker's services
        public const int HOOKER_SERVICE_BASIC = 0;
        public const int HOOKER_SERVICE_FULL = 1;

        // Alcohol limit
        public const float WASTED_LEVEL = 0.4f;

        // Generic interiors
        public static List<InteriorModel> INTERIOR_LIST = new List<InteriorModel>
        {
            new InteriorModel(Messages.GEN_TOWNHALL, new Vector3(-1285.544f, -567.0439f, 31.71239f), new Vector3(-141.1987f, -620.913f, 168.8205f), "ex_dt1_02_office_02b", 498, Messages.GEN_TOWNHALL),
            new InteriorModel(Messages.GEN_HOSPITAL, new Vector3(-1385.481f, -976.4036f, 9.273162f), new Vector3(275.446f, -1361.11f, 24.5378f), "Coroner_Int_On", 153, Messages.GEN_HOSPITAL),
            new InteriorModel(Messages.GEN_DRIVING_SCHOOL, new Vector3(-177.19, -1158.32, 23.81), new Vector3(-177.19, -1158.32, 23.81), "", 269, Messages.GEN_DRIVING_SCHOOL),
            new InteriorModel(Messages.GEN_WEAZEL_NEWS, new Vector3(-598.51, -929.95, 23.87), new Vector3(-1082.433f, -258.7667f, 37.76331f), "facelobby", 459, Messages.GEN_WEAZEL_NEWS),
            new InteriorModel(Messages.GEN_POLICE_STATION, new Vector3(-1111.952f, -824.9194f, 19.31578f), new Vector3(435.4738f, -981.7497f, 30.69148f), string.Empty, 60, Messages.GEN_POLICE_STATION),
        };

        // Business IPLs from the game
        public static List<BusinessIplModel> BUSINESS_IPL_LIST = new List<BusinessIplModel>
        {
            new BusinessIplModel(BUSINESS_TYPE_24_7, "ipl_supermarket", new Vector3(-710.1048f, -914.5465f, 19.21559f)),
            new BusinessIplModel(BUSINESS_TYPE_ELECTRONICS, "ex_exec_warehouse_placement_interior_2_int_warehouse_l_dlc_milo", new Vector3(1026.751f, -3101.307f, -38.99986f)),
            new BusinessIplModel(BUSINESS_TYPE_HARDWARE, "v_chopshop", new Vector3(481.9714f, -1313.103f, 29.20123f)),
            new BusinessIplModel(BUSINESS_TYPE_CLOTHES, "ipl_clothes", new Vector3(126.5524f, -212.5681f, 54.55783f)),
            new BusinessIplModel(BUSINESS_TYPE_DISCO, "v_bahama", new Vector3(-1387.981f, -587.6373f, 30.31952f)),
            new BusinessIplModel(BUSINESS_TYPE_WAREHOUSE, "v_recycle", new Vector3(-593.5312f, -1630.137f, 27.01079f)),
            new BusinessIplModel(BUSINESS_TYPE_AMMUNATION, "ipl_ammu", new Vector3(1698.488f, 3752.896f, 34.70532f)),
            new BusinessIplModel(BUSINESS_TYPE_BAR, "v_rockclub", new Vector3(-564.4153f, 277.4367f, 83.13631f)),
            new BusinessIplModel(BUSINESS_TYPE_JEWELRY, "post_hiest_unload", new Vector3(-630.4483f, -236.8936f, 38.05701f)),
            new BusinessIplModel(BUSINESS_TYPE_PRIVATE_OFFICE, "v_psycheoffice", new Vector3(-1906.785f, -573.757f, 19.077f)),
            new BusinessIplModel(BUSINESS_TYPE_CLUBHOUSE, "bkr_bi_hw1_13_int", new Vector3(982.4059f, -100.1532f, 74.84502f)),
            new BusinessIplModel(BUSINESS_TYPE_GAS_STATION, "ipl_supermarket", new Vector3(-710.1048f, -914.5465f, 19.21559f)),
            new BusinessIplModel(BUSINESS_TYPE_SLAUGHTERHOUSE, "ipl_slaughterhouse", new Vector3(964.3511f, -2185.115f, 30.30081f)),
            new BusinessIplModel(BUSINESS_TYPE_BARBER_SHOP, "barber_shop", new Vector3(133.9966f, -1710.311f, 29.29162f)),
            new BusinessIplModel(BUSINESS_TYPE_FACTORY, "id2_14_during1", new Vector3(717.0f, -975.0f, 25.0f)),
            new BusinessIplModel(BUSINESS_TYPE_TORTURE_ROOM, "v_torture", new Vector3(135.7002f, -2203.643f, 7.309135f)),
            new BusinessIplModel(BUSINESS_TYPE_GARAGE_LOW_END, "low_end_garage_no_ipl", new Vector3(178.8302f, -1000.515f, -98.99998f)),
            new BusinessIplModel(BUSINESS_TYPE_WAREHOUSE_MEDIUM, "ex_exec_warehouse_placement_interior_0_int_warehouse_m_dlc_milo", new Vector3(1048.286f, -3096.858f, -38.99991f)),
            new BusinessIplModel(BUSINESS_TYPE_SOCIAL_CLUB, "house_no_ipl_a", new Vector3(265.9776f, -1006.97f, -100.8839f)),
            new BusinessIplModel(BUSINESS_TYPE_TATTOO_SHOP, "business_no_ipl", new Vector3(-1154.249f, -1424.721f, 4.954462f)),
            new BusinessIplModel(BUSINESS_TYPE_BENNYS_WHORKSHOP, "business_no_ipl2", new Vector3(-205.4454f, -1312.916f, 31.13982f)),
            new BusinessIplModel(BUSINESS_TYPE_MECHANIC, "v_chopshop", new Vector3(481.9714f, -1313.103f, 29.20123f)),
            new BusinessIplModel(BUSINESS_TYPE_VANILLA, "vanilla_no_ipl", new Vector3(128.9892f, -1296.068f, 29.26953f)),
            new BusinessIplModel(BUSINESS_TYPE_FISHING, "ex_exec_warehouse_placement_interior_0_int_warehouse_m_dlc_milo", new Vector3(1048.286f, -3096.858f, -38.99991f))
        };

        // House interiors from the game
        public static List<HouseIplModel> HOUSE_IPL_LIST = new List<HouseIplModel>
        {
            // Apartments with IPL
            new HouseIplModel("apa_v_mp_h_01_a", new Vector3(-786.8663f, 315.7642f, 217.6385f)),
            new HouseIplModel("apa_v_mp_h_01_c", new Vector3(-786.9563f, 315.6229f, 187.9136f)),
            new HouseIplModel("apa_v_mp_h_01_b", new Vector3(-774.0126f, 342.0428f, 196.6864f)),
            new HouseIplModel("apa_v_mp_h_02_a", new Vector3(-787.0749f, 315.8198f, 217.6386f)),
            new HouseIplModel("apa_v_mp_h_02_c", new Vector3(-786.8195f, 315.5634f, 187.9137f)),
            new HouseIplModel("apa_v_mp_h_02_b", new Vector3(-774.1382f, 342.0316f, 196.6864f)),
            new HouseIplModel("apa_v_mp_h_03_a", new Vector3(-786.6245f, 315.6175f, 217.6385f)),
            new HouseIplModel("apa_v_mp_h_03_c", new Vector3(-786.9584f, 315.7974f, 187.9135f)),
            new HouseIplModel("apa_v_mp_h_03_b", new Vector3(-774.0223f, 342.1718f, 196.6863f)),
            new HouseIplModel("apa_v_mp_h_04_a", new Vector3(-787.0902f, 315.7039f, 217.6384f)),
            new HouseIplModel("apa_v_mp_h_04_c", new Vector3(-787.0155f, 315.7071f, 187.9135f)),
            new HouseIplModel("apa_v_mp_h_04_b", new Vector3(-773.8976f, 342.1525f, 196.6863f)),
            new HouseIplModel("apa_v_mp_h_05_a", new Vector3(-786.9887f, 315.7393f, 217.6386f)),
            new HouseIplModel("apa_v_mp_h_05_c", new Vector3(-786.8809f, 315.6634f, 187.9136f)),
            new HouseIplModel("apa_v_mp_h_05_b", new Vector3(-774.0675f, 342.0773f, 196.6864f)),
            new HouseIplModel("apa_v_mp_h_06_a", new Vector3(-787.1423f, 315.6943f, 217.6384f)),
            new HouseIplModel("apa_v_mp_h_06_c", new Vector3(-787.0961f, 315.815f, 187.9135f)),
            new HouseIplModel("apa_v_mp_h_06_b", new Vector3(-773.9552f, 341.9892f, 196.6862f)),
            new HouseIplModel("apa_v_mp_h_07_a", new Vector3(-787.029f, 315.7113f, 217.6385f)),
            new HouseIplModel("apa_v_mp_h_07_c", new Vector3(-787.0574f, 315.6567f, 187.9135f)),
            new HouseIplModel("apa_v_mp_h_07_b", new Vector3(-774.0109f, 342.0965f, 196.6863f)),
            new HouseIplModel("apa_v_mp_h_08_a", new Vector3(-786.9469f, 315.5655f, 217.6383f)),
            new HouseIplModel("apa_v_mp_h_08_c", new Vector3(-786.9756f, 315.723f, 187.9134f)),
            new HouseIplModel("apa_v_mp_h_08_b", new Vector3(-774.0349f, 342.0296f, 196.6862f)),

            // Apartments without IPL
            new HouseIplModel("house_no_ipl_a", new Vector3(265.9776f, -1006.97f, -100.8839f)),
            new HouseIplModel("house_no_ipl_b", new Vector3(-30.58078f, -595.3096f, 80.03086f)),
            new HouseIplModel("house_no_ipl_c", new Vector3(-30.58078f, -595.3096f, 80.03086f)),
            new HouseIplModel("house_no_ipl_d", new Vector3(-17.72512f, -588.8995f, 90.1148f)),
            new HouseIplModel("house_no_ipl_e", new Vector3(-1451.652f, -523.7687f, 56.92904f)),
            new HouseIplModel("house_no_ipl_f", new Vector3(-1451.652f, -523.7687f, 56.92904f)),
            new HouseIplModel("house_no_ipl_g", new Vector3(-1451.652f, -523.7687f, 56.92904f)),
            new HouseIplModel("house_no_ipl_h", new Vector3(-1451.652f, -523.7687f, 56.92904f)),
            new HouseIplModel("house_no_ipl_i", new Vector3(-174.2659f, 497.3836f, 137.667f)),
            new HouseIplModel("house_no_ipl_j", new Vector3(-1451.652f, -523.7687f, 56.92904f)),
            new HouseIplModel("house_no_ipl_k", new Vector3(-1451.652f, -523.7687f, 56.92904f)),
            new HouseIplModel("house_no_ipl_l", new Vector3(-1451.652f, -523.7687f, 56.92904f)),
            new HouseIplModel("house_no_ipl_m", new Vector3(-1451.652f, -523.7687f, 56.92904f)),
            new HouseIplModel("house_no_ipl_n", new Vector3(-1451.652f, -523.7687f, 56.92904f)),
            new HouseIplModel("house_no_ipl_o", new Vector3(-1451.652f, -523.7687f, 56.92904f)),
            new HouseIplModel("house_no_ipl_p", new Vector3(-1451.652f, -523.7687f, 56.92904f)),

            // Trevor's trailer
            new HouseIplModel("TrevorsMP", new Vector3(1972.965f, 3816.529f, 33.42873f)),
            new HouseIplModel("TrevorsTrailerTidy", new Vector3(1972.965f, 3816.529f, 33.42873f)),

            // Floyd's house
            new HouseIplModel("vb_30_crimetape", new Vector3(-1149.709f, -1521.088f, 10.78267f)),

            // Lester's house
            new HouseIplModel("lester_house", new Vector3(1273.9f, -1719.305f, 54.77141f)),

            // Janitor's office
            new HouseIplModel("v_janitor", new Vector3(-107.6496f, -8.308348f, 70.51957f)),

            // Mansion
            new HouseIplModel("mansion_no_ipl", new Vector3(1396.415f, 1141.854f, 114.3336f)),

            // Flat with rooms
            new HouseIplModel("flat_no_ipl", new Vector3(346.3212f, -1012.968f, -99.19625f)),

            // Franklin's aunt's house
            new HouseIplModel("franklin_hoo_house", new Vector3(-14.31764f, -1439.986f, 31.10155f)),

            // Franklin's mansion
            new HouseIplModel("franklin_mansion_no_ipl", new Vector3(7.69007f, 538.0661f, 176.028f)),

            // O'Neill's farm
            new HouseIplModel("farmint", new Vector3(2436.875f, 4974.916f, 46.8106f)),

            // Motel room
            new HouseIplModel("motel_room_no_ipl", new Vector3(151.1905f, -1007.731f, -98.99999f))
        };

        // Faction ranks
        public static List<FactionModel> FACTION_RANK_LIST = new List<FactionModel>
        {
            new FactionModel(Messages.FAC_NONE_M, Messages.FAC_NONE_F, FACTION_NONE, 0, 0),

            // Police department
            new FactionModel(Messages.FAC_LSPD_0_M, Messages.FAC_LSPD_0_F, FACTION_POLICE, 0, 0),
            new FactionModel(Messages.FAC_LSPD_1_M, Messages.FAC_LSPD_1_F, FACTION_POLICE, 1, 1250),
            new FactionModel(Messages.FAC_LSPD_2_M, Messages.FAC_LSPD_2_F, FACTION_POLICE, 2, 1388),
            new FactionModel(Messages.FAC_LSPD_3_M, Messages.FAC_LSPD_3_F, FACTION_POLICE, 3, 1685),
            new FactionModel(Messages.FAC_LSPD_4_M, Messages.FAC_LSPD_4_F, FACTION_POLICE, 4, 2056),
            new FactionModel(Messages.FAC_LSPD_5_M, Messages.FAC_LSPD_5_F, FACTION_POLICE, 5, 2420),
            new FactionModel(Messages.FAC_LSPD_6_M, Messages.FAC_LSPD_6_F, FACTION_POLICE, 6, 2901),
            new FactionModel(Messages.FAC_LSPD_7_M, Messages.FAC_LSPD_7_F, FACTION_POLICE, 7, 2200),

            // Emergency department
            new FactionModel(Messages.FAC_EMS_1_M, Messages.FAC_EMS_1_F, FACTION_EMERGENCY, 1, 1075),
            new FactionModel(Messages.FAC_EMS_2_M, Messages.FAC_EMS_2_F, FACTION_EMERGENCY, 2, 1200),
            new FactionModel(Messages.FAC_EMS_3_M, Messages.FAC_EMS_3_F, FACTION_EMERGENCY, 3, 1500),
            new FactionModel(Messages.FAC_EMS_4_M, Messages.FAC_EMS_4_F, FACTION_EMERGENCY, 4, 1500),
            new FactionModel(Messages.FAC_EMS_5_M, Messages.FAC_EMS_5_F, FACTION_EMERGENCY, 5, 1800),
            new FactionModel(Messages.FAC_EMS_6_M, Messages.FAC_EMS_6_F, FACTION_EMERGENCY, 6, 1800),
            new FactionModel(Messages.FAC_EMS_7_M, Messages.FAC_EMS_7_F, FACTION_EMERGENCY, 7, 2200),
            new FactionModel(Messages.FAC_EMS_8_M, Messages.FAC_EMS_8_F, FACTION_EMERGENCY, 8, 2200),
            new FactionModel(Messages.FAC_EMS_9_M, Messages.FAC_EMS_9_F, FACTION_EMERGENCY, 9, 2800),
            new FactionModel(Messages.FAC_EMS_10_M, Messages.FAC_EMS_10_F, FACTION_EMERGENCY, 10, 3500),

            // News
            new FactionModel(Messages.FAC_NEWS_1_M, Messages.FAC_NEWS_1_F, FACTION_NEWS, 1, 1020),
            new FactionModel(Messages.FAC_NEWS_2_M, Messages.FAC_NEWS_2_F, FACTION_NEWS, 2, 1100),
            new FactionModel(Messages.FAC_NEWS_3_M, Messages.FAC_NEWS_3_F, FACTION_NEWS, 3, 1200),
            new FactionModel(Messages.FAC_NEWS_4_M, Messages.FAC_NEWS_4_F, FACTION_NEWS, 4, 1610),
            new FactionModel(Messages.FAC_NEWS_5_M, Messages.FAC_NEWS_5_F, FACTION_NEWS, 5, 2300),

            // Town hall
            new FactionModel(Messages.FAC_TOWN_1_M, Messages.FAC_TOWN_1_F, FACTION_TOWNHALL, 1, 1200),
            new FactionModel(Messages.FAC_TOWN_2_M, Messages.FAC_TOWN_2_F, FACTION_TOWNHALL, 2, 1800),
            new FactionModel(Messages.FAC_TOWN_3_M, Messages.FAC_TOWN_3_F, FACTION_TOWNHALL, 3, 2200),
            new FactionModel(Messages.FAC_TOWN_4_M, Messages.FAC_TOWN_4_F, FACTION_TOWNHALL, 4, 3000),

            // Transport services
            new FactionModel(Messages.FAC_LSTD_1_M, Messages.FAC_LSTD_1_F, FACTION_TAXI_DRIVER, 1, 1020),
            new FactionModel(Messages.FAC_LSTD_2_M, Messages.FAC_LSTD_2_F, FACTION_TAXI_DRIVER, 2, 1180),
            new FactionModel(Messages.FAC_LSTD_3_M, Messages.FAC_LSTD_3_F, FACTION_TAXI_DRIVER, 3, 1360),
            new FactionModel(Messages.FAC_LSTD_4_M, Messages.FAC_LSTD_4_F, FACTION_TAXI_DRIVER, 4, 1600),
            new FactionModel(Messages.FAC_LSTD_5_M, Messages.FAC_LSTD_5_F, FACTION_TAXI_DRIVER, 5, 1890)
        };

        // Job description and salary
        public static List<JobModel> JOB_LIST = new List<JobModel>
        {
            new JobModel(Messages.JOB_UNEMPLOYED_M, Messages.JOB_UNEMPLOYED_F, JOB_NONE, 575),
            new JobModel(Messages.JOB_FASTFOOD_M, Messages.JOB_FASTFOOD_F, JOB_FASTFOOD, 775),
            new JobModel(Messages.JOB_THIEF_M, Messages.JOB_THIEF_F, JOB_THIEF, 450),
            new JobModel(Messages.JOB_MECHANIC_M, Messages.JOB_MECHANIC_F, JOB_MECHANIC, 875),
            new JobModel(Messages.JOB_GARGAGE_M, Messages.JOB_GARGAGE_F, JOB_GARBAGE, 975),
            new JobModel(Messages.JOB_HOOKER_M, Messages.JOB_HOOKER_F, JOB_HOOKER, 575)
        };

        // Uniform list
        public static List<UniformModel> UNIFORM_LIST = new List<UniformModel>
        {  
            // Male police uniform
            new UniformModel(0, FACTION_POLICE, SEX_MALE, 0, 0, 0),
            new UniformModel(0, FACTION_POLICE, SEX_MALE, 1, 0, 0),
            new UniformModel(0, FACTION_POLICE, SEX_MALE, 2, -1, -1),
            new UniformModel(0, FACTION_POLICE, SEX_MALE, 3, 0, 0),
            new UniformModel(0, FACTION_POLICE, SEX_MALE, 4, 35, 0),
            new UniformModel(0, FACTION_POLICE, SEX_MALE, 5, 0, 0),
            new UniformModel(0, FACTION_POLICE, SEX_MALE, 6, 25, 0),
            new UniformModel(0, FACTION_POLICE, SEX_MALE, 7, 0, 0),
            new UniformModel(0, FACTION_POLICE, SEX_MALE, 8, 58, 0),
            new UniformModel(0, FACTION_POLICE, SEX_MALE, 9, 0, 0),
            new UniformModel(0, FACTION_POLICE, SEX_MALE, 10, 0, 0),
            new UniformModel(0, FACTION_POLICE, SEX_MALE, 11, 55, 0),

            // Female police uniform
            new UniformModel(0, FACTION_POLICE, SEX_FEMALE, 0, 0, 0),
            new UniformModel(0, FACTION_POLICE, SEX_FEMALE, 1, 0, 0),
            new UniformModel(0, FACTION_POLICE, SEX_FEMALE, 2, -1, -1),
            new UniformModel(0, FACTION_POLICE, SEX_FEMALE, 3, 14, 0),
            new UniformModel(0, FACTION_POLICE, SEX_FEMALE, 4, 34, 0),
            new UniformModel(0, FACTION_POLICE, SEX_FEMALE, 5, 0, 0),
            new UniformModel(0, FACTION_POLICE, SEX_FEMALE, 6, 25, 0),
            new UniformModel(0, FACTION_POLICE, SEX_FEMALE, 7, 0, 0),
            new UniformModel(0, FACTION_POLICE, SEX_FEMALE, 8, 35, 0),
            new UniformModel(0, FACTION_POLICE, SEX_FEMALE, 9, 0, 0),
            new UniformModel(0, FACTION_POLICE, SEX_FEMALE, 10, 0, 0),
            new UniformModel(0, FACTION_POLICE, SEX_FEMALE, 11, 48, 0),

            // Male paramedic uniform
            new UniformModel(0, FACTION_EMERGENCY, SEX_MALE, 0, -1, -1),
            new UniformModel(0, FACTION_EMERGENCY, SEX_MALE, 1, 0, 0),
            new UniformModel(0, FACTION_EMERGENCY, SEX_MALE, 2, -1, -1),
            new UniformModel(0, FACTION_EMERGENCY, SEX_MALE, 3, 90, 0),
            new UniformModel(0, FACTION_EMERGENCY, SEX_MALE, 4, 96, 0),
            new UniformModel(0, FACTION_EMERGENCY, SEX_MALE, 5, -1, -1),
            new UniformModel(0, FACTION_EMERGENCY, SEX_MALE, 6, 51, 0),
            new UniformModel(0, FACTION_EMERGENCY, SEX_MALE, 7, 126, 0),
            new UniformModel(0, FACTION_EMERGENCY, SEX_MALE, 8, 15, 0),
            new UniformModel(0, FACTION_EMERGENCY, SEX_MALE, 9, 0, 0),
            new UniformModel(0, FACTION_EMERGENCY, SEX_MALE, 10, 57, 0),
            new UniformModel(0, FACTION_EMERGENCY, SEX_MALE, 11,249, 0),

            // Female paramedic uniform
            new UniformModel(0, FACTION_EMERGENCY, SEX_FEMALE, 0, -1, -1),
            new UniformModel(0, FACTION_EMERGENCY, SEX_FEMALE, 1, 0, 0),
            new UniformModel(0, FACTION_EMERGENCY, SEX_FEMALE, 2, -1, -1),
            new UniformModel(0, FACTION_EMERGENCY, SEX_FEMALE, 3, 85, 0),
            new UniformModel(0, FACTION_EMERGENCY, SEX_FEMALE, 4, 96, 0),
            new UniformModel(0, FACTION_EMERGENCY, SEX_FEMALE, 5, -1, -1),
            new UniformModel(0, FACTION_EMERGENCY, SEX_FEMALE, 6, 51, 0),
            new UniformModel(0, FACTION_EMERGENCY, SEX_FEMALE, 7, 127, 0),
            new UniformModel(0, FACTION_EMERGENCY, SEX_FEMALE, 8, 129, 0),
            new UniformModel(0, FACTION_EMERGENCY, SEX_FEMALE, 9, 0, 0),
            new UniformModel(0, FACTION_EMERGENCY, SEX_FEMALE, 10, 58, 0),
            new UniformModel(0, FACTION_EMERGENCY, SEX_FEMALE, 11, 250, 0)
        };

        // Guns
        public static List<GunModel> GUN_LIST = new List<GunModel>()
        {
            // Pistols
            new GunModel(WeaponHash.Pistol, ITEM_HASH_PISTOL_AMMO_CLIP, 12),
            new GunModel(WeaponHash.CombatPistol, ITEM_HASH_PISTOL_AMMO_CLIP, 12),
            new GunModel(WeaponHash.Pistol50, ITEM_HASH_PISTOL_AMMO_CLIP, 9),
            new GunModel(WeaponHash.SNSPistol, ITEM_HASH_PISTOL_AMMO_CLIP, 6),
            new GunModel(WeaponHash.HeavyPistol, ITEM_HASH_PISTOL_AMMO_CLIP, 18),
            new GunModel(WeaponHash.VintagePistol, ITEM_HASH_PISTOL_AMMO_CLIP, 7),
            new GunModel(WeaponHash.MarksmanPistol, ITEM_HASH_PISTOL_AMMO_CLIP, 1),
            new GunModel(WeaponHash.Revolver, ITEM_HASH_PISTOL_AMMO_CLIP, 6),
            new GunModel(WeaponHash.APPistol, ITEM_HASH_PISTOL_AMMO_CLIP, 18),
            new GunModel(WeaponHash.FlareGun, ITEM_HASH_PISTOL_AMMO_CLIP, 1),

            // Machine guns
            new GunModel(WeaponHash.MicroSMG, ITEM_HASH_MACHINEGUN_AMMO_CLIP, 16),
            new GunModel(WeaponHash.MachinePistol, ITEM_HASH_MACHINEGUN_AMMO_CLIP, 12),
            new GunModel(WeaponHash.SMG, ITEM_HASH_MACHINEGUN_AMMO_CLIP, 30),
            new GunModel(WeaponHash.AssaultSMG, ITEM_HASH_MACHINEGUN_AMMO_CLIP, 30),
            new GunModel(WeaponHash.CombatPDW, ITEM_HASH_MACHINEGUN_AMMO_CLIP, 30),
            new GunModel(WeaponHash.MG, ITEM_HASH_MACHINEGUN_AMMO_CLIP, 54),
            new GunModel(WeaponHash.CombatMG, ITEM_HASH_MACHINEGUN_AMMO_CLIP, 100),
            new GunModel(WeaponHash.Gusenberg, ITEM_HASH_MACHINEGUN_AMMO_CLIP, 30),
            new GunModel(WeaponHash.MiniSMG, ITEM_HASH_MACHINEGUN_AMMO_CLIP, 20),

            // Assault rifles
            new GunModel(WeaponHash.AssaultRifle, ITEM_HASH_ASSAULTRIFLE_AMMO_CLIP, 30),
            new GunModel(WeaponHash.CarbineRifle, ITEM_HASH_ASSAULTRIFLE_AMMO_CLIP, 30),
            new GunModel(WeaponHash.AdvancedRifle, ITEM_HASH_ASSAULTRIFLE_AMMO_CLIP, 30),
            new GunModel(WeaponHash.SpecialCarbine, ITEM_HASH_ASSAULTRIFLE_AMMO_CLIP, 30),
            new GunModel(WeaponHash.BullpupRifle, ITEM_HASH_ASSAULTRIFLE_AMMO_CLIP, 30),
            new GunModel(WeaponHash.CompactRifle, ITEM_HASH_ASSAULTRIFLE_AMMO_CLIP, 30),

            // Sniper rifles
            new GunModel(WeaponHash.SniperRifle, ITEM_HASH_SNIPERRIFLE_AMMO_CLIP, 10),
            new GunModel(WeaponHash.HeavySniper, ITEM_HASH_SNIPERRIFLE_AMMO_CLIP, 6),
            new GunModel(WeaponHash.MarksmanRifle, ITEM_HASH_SNIPERRIFLE_AMMO_CLIP, 8),

            // Shotguns
            new GunModel(WeaponHash.PumpShotgun, ITEM_HASH_SHOTGUN_AMMO_CLIP, 8),
            new GunModel(WeaponHash.SawnOffShotgun, ITEM_HASH_SHOTGUN_AMMO_CLIP, 8),
            new GunModel(WeaponHash.BullpupShotgun, ITEM_HASH_SHOTGUN_AMMO_CLIP, 14),
            new GunModel(WeaponHash.AssaultShotgun, ITEM_HASH_SHOTGUN_AMMO_CLIP, 8),
            new GunModel(WeaponHash.Musket, ITEM_HASH_SHOTGUN_AMMO_CLIP, 1),
            new GunModel(WeaponHash.HeavyShotgun, ITEM_HASH_SHOTGUN_AMMO_CLIP, 6),
            new GunModel(WeaponHash.DoubleBarrelShotgun, ITEM_HASH_SHOTGUN_AMMO_CLIP, 2)
        };

        // Crime list
        public static List<CrimeModel> CRIME_LIST = new List<CrimeModel>()
        {
            new CrimeModel("Omisión de indicaciones policiales", 20, 500, "Quitar 2 puntos del carnet"),
            new CrimeModel("Conducción en estado alterado", 10, 800, "Quitar 3 puntos del carnet"),
            new CrimeModel("Conducción temeraria sin víctimas", 10, 500, "Quitar 3 puntos del carnet"),
            new CrimeModel("Conducción temeraria con víctimas", 25, 1000, "Retirada del carnet"),
            new CrimeModel("Atropello sin fuga", 10, 1000, "Quitar 6 puntos del carnet"),
            new CrimeModel("Atropello con fuga", 20, 2000, "Retirada del carnet"),
            new CrimeModel("Accidente y huida", 15, 1500, "Quitar 6 puntos del carnet"),
            new CrimeModel("Participación en carreras ilegales", 25, 4500, "Retirada del carnet"),
            new CrimeModel("Matrícula no registrada", 30, 5000, "Retirada del carnet y del vehículo"),
            new CrimeModel("Alteración del orden público", 15, 300, ""),
            new CrimeModel("Desacato a la autoridad", 20, 900, ""),
            new CrimeModel("Obstrucción de la justicia", 10, 700, ""),
            new CrimeModel("Huida a pie tras advertencia", 10, 800, ""),
            new CrimeModel("Destrucción del mobiliario público", 20, 1000, ""),
            new CrimeModel("Agresión a civil sin armas", 15, 800, ""),
            new CrimeModel("Agresión a civil con armas", 20, 2500, ""),
            new CrimeModel("Agresión a un agente sin armas", 30, 3000, ""),
            new CrimeModel("Agresión a un agente con armas", 60, 5000, ""),
            new CrimeModel("Estafa o fraude", 20, 4000, ""),
            new CrimeModel("Complice de rapto", 30, 4000, ""),
            new CrimeModel("Rapto", 60, 8000, ""),
            new CrimeModel("Complice de homicidio", 30, 10000, ""),
            new CrimeModel("Homicidio", 60, 15000, ""),
            new CrimeModel("Homicidio a un agente", 120, 40000, ""),
            new CrimeModel("Llamada falsa a emergencias", 15, 1000, ""),
            new CrimeModel("Intento de homicidio", 60, 10000, ""),
            new CrimeModel("Intento de homicidio a un agente", 60, 15000, ""),
            new CrimeModel("Atraco a civil", 15, 500, "Retirada de los artículos robados"),
            new CrimeModel("Atraco a local", 20, 600, "Retirada de los artículos robados"),
            new CrimeModel("Robo en domicilio", 20, 600, "Retirada de los artículos robados"),
            new CrimeModel("Robo en vehículo", 20, 600, "Retirada de los artículos robados"),
            new CrimeModel("Atraco o robo con fuga", 20, 800, ""),
            new CrimeModel("Cómplice de atraco o robo", 10, 400, ""),
            new CrimeModel("Atraco a una entidad financiera", 30, 6000, ""),
            new CrimeModel("Allanamiento de morada", 15, 1000, ""),
            new CrimeModel("Posesión de arma sin licencia", 60, 10000, "Retirada del arma"),
            new CrimeModel("Uso indebido de armas con licencia", 60, 20000, "Retirada de la licencia"),
            new CrimeModel("Suplantación de indentidad", 50, 10000, ""),
            new CrimeModel("Suplantación de departamento estatal", 100, 20000, "")
        };

        // Jail positions
        public static List<Vector3> JAIL_SPAWNS = new List<Vector3>
        {
            // Cells
            new Vector3(460.0685f, -993.9847f, 24.91487f),
            new Vector3(459.6115f, -998.0204f, 24.91487f),
            new Vector3(459.8612f, -1001.641f, 24.91487f),

            // IC jail's exit
            new Vector3(463.6655f, -990.8979f, 24.91487f),

            // OOC jail's exit
            new Vector3(-1285.544f, -567.0439f, 31.71239f)
        };

        // Business sellable items
        public static List<BusinessItemModel> BUSINESS_ITEM_LIST = new List<BusinessItemModel>
        {
            // 24-7
            new BusinessItemModel(Messages.ITM_BEER_BOTTLE, ITEM_HASH_BOTTLE_BEER_AM, ITEM_TYPE_CONSUMABLE, 10, 0.1f, 1, 1, new Vector3(0.05f, -0.02f, -0.02f), new Vector3(270.0f, 0.0f, 0.0f), BUSINESS_TYPE_24_7, 0.08f),
            new BusinessItemModel(Messages.ITM_BEER_PACK, ITEM_HASH_PACK_BEER_AM, ITEM_TYPE_OPENABLE, 60, 0.1f, 0, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_24_7, 0.0f),
            new BusinessItemModel(Messages.ITM_SANDWICH, ITEM_HASH_SANDWICH, ITEM_TYPE_CONSUMABLE, 5, 0.1f, 10, 1, new Vector3(0.06f, 0.0f, -0.02f), new Vector3(180.0f, 180.0f, 90.0f), BUSINESS_TYPE_24_7, 0.0f),
            new BusinessItemModel(Messages.ITM_CIGARETTES, ITEM_HASH_CIGARRETES_PACK_OPEN, ITEM_TYPE_CONSUMABLE, 8, 0.1f, -2, 1, new Vector3(0.06f, 0.0f, -0.02f), new Vector3(270.0f, 0.0f, 0.0f), BUSINESS_TYPE_24_7, 0.0f),
            new BusinessItemModel(Messages.ITM_COLA, ITEM_HASH_CAN_COLA, ITEM_TYPE_CONSUMABLE, 5, 0.1f, 5, 1, new Vector3(0.05f, -0.03f, 0.0f), new Vector3(270.0f, 20.0f, -20.0f), BUSINESS_TYPE_24_7, 0.0f),
            new BusinessItemModel(Messages.ITM_CANDY, ITEM_HASH_CANDY, ITEM_TYPE_CONSUMABLE, 4, 0.1f, 3, 1, new Vector3(0.05f, -0.010f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_24_7, 0.0f),
            new BusinessItemModel(Messages.ITM_JERRYCAN, ITEM_HASH_JERRYCAN, ITEM_TYPE_EQUIPABLE, 25, 0.1f, 0, 1, new Vector3(0.09f, 0.09f, 0.0f), new Vector3(0.0f, 90.0f, 175.0f), BUSINESS_TYPE_24_7, 0.0f),
            new BusinessItemModel(Messages.ITM_COFFEE, ITEM_HASH_CUP_COFFEE, ITEM_TYPE_CONSUMABLE, 10, 0.1f, 5, 1, new Vector3(0.05f, -0.02f, -0.02f), new Vector3(270.0f, 0.0f, 0.0f), BUSINESS_TYPE_24_7, 0.0f),
 
            // Oil station
            new BusinessItemModel(Messages.ITM_JERRYCAN, ITEM_HASH_JERRYCAN, ITEM_TYPE_EQUIPABLE, 25, 0.1f, 0, 1, new Vector3(0.09f, 0.09f, 0.0f), new Vector3(0.0f, 90.0f, 175.0f), BUSINESS_TYPE_GAS_STATION, 0.0f),
 
            // Electronic store
            new BusinessItemModel(Messages.ITM_SMARTPHONE, ITEM_HASH_TELEPHONE, ITEM_TYPE_EQUIPABLE, 200, 0.1f, 0, 1, new Vector3(0.06f, 0.0f, -0.02f), new Vector3(270.0f, 0.0f, 0.0f), BUSINESS_TYPE_ELECTRONICS, 0.0f),
            new BusinessItemModel(Messages.ITM_WALKIE, ITEM_HASH_WALKIE, ITEM_TYPE_EQUIPABLE, 150, 0.1f, 0, 1, new Vector3(0.06f, 0.0f, -0.02f), new Vector3(270.0f, 0.0f, 0.0f), BUSINESS_TYPE_ELECTRONICS, 0.0f),
            new BusinessItemModel(Messages.ITM_CAMERA, ITEM_HASH_CAMERA, ITEM_TYPE_EQUIPABLE, 50, 0.1f, 0, 1, new Vector3(0.05f, -0.02f, -0.02f), new Vector3(270.0f, 0.0f, 0.0f), BUSINESS_TYPE_ELECTRONICS, 0.0f),

            // Hardware store
            new BusinessItemModel(Messages.ITM_CROWBAR, WeaponHash.Crowbar.ToString(), ITEM_TYPE_WEAPON, 60, 0.1f, 0, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_HARDWARE, 0.0f),
            new BusinessItemModel(Messages.ITM_HAMMER, WeaponHash.Hammer.ToString(), ITEM_TYPE_WEAPON, 50, 0.1f, 0, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_HARDWARE, 0.0f),
            new BusinessItemModel(Messages.ITM_FLASHLIGHT, WeaponHash.Flashlight.ToString(), ITEM_TYPE_WEAPON, 30, 0.1f, 0, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_HARDWARE, 0.0f),
            new BusinessItemModel(Messages.ITM_HATCHET, WeaponHash.Hatchet.ToString(), ITEM_TYPE_WEAPON, 200, 0.1f, 0, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_HARDWARE, 0.0f),
            new BusinessItemModel(Messages.ITM_WRENCH, WeaponHash.Wrench.ToString(), ITEM_TYPE_WEAPON, 100, 0.1f, 0, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_HARDWARE, 0.0f),
            new BusinessItemModel(Messages.ITM_KNUCLE_DUSTER, WeaponHash.KnuckleDuster.ToString(), ITEM_TYPE_WEAPON, 100, 0.1f, 0, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_HARDWARE, 0.0f),
            new BusinessItemModel(Messages.ITM_KNIFE, WeaponHash.Knife.ToString(), ITEM_TYPE_WEAPON, 250, 0.1f, 0, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_HARDWARE, 0.0f),
            new BusinessItemModel(Messages.ITM_SWITCHBLADE, WeaponHash.SwitchBlade.ToString(), ITEM_TYPE_WEAPON, 150, 0.1f, 0, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_HARDWARE, 0.0f),
            new BusinessItemModel(Messages.ITM_BAT, WeaponHash.Bat.ToString(), ITEM_TYPE_WEAPON, 50, 0.1f, 0, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_HARDWARE, 0.0f),
 
            // Bar
            new BusinessItemModel(Messages.ITM_BEER_BOTTLE, ITEM_HASH_BOTTLE_BEER_PISSWASSER, ITEM_TYPE_CONSUMABLE, 10, 0.1f, 1, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(270.0f, 0.0f, 0.0f), BUSINESS_TYPE_BAR, 0.08f),
            new BusinessItemModel(Messages.ITM_BURGER, ITEM_HASH_BURGER, ITEM_TYPE_CONSUMABLE, 5, 0.1f, 20, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_BAR, 0.0f),
            new BusinessItemModel(Messages.ITM_COFFEE, ITEM_HASH_CUP_COFFEE, ITEM_TYPE_CONSUMABLE, 10, 0.1f, 5, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(270.0f, 0.0f, 0.0f), BUSINESS_TYPE_BAR, 0.0f),
            new BusinessItemModel(Messages.ITM_COLA, ITEM_HASH_CAN_COLA, ITEM_TYPE_CONSUMABLE, 5, 0.1f, 5, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(270.0f, 0.0f, 0.0f), BUSINESS_TYPE_BAR, 0.0f),
            new BusinessItemModel(Messages.ITM_HOTDOG, ITEM_HASH_HOTDOG, ITEM_TYPE_CONSUMABLE, 2, 0.1f, 15, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(270.0f, 0.0f, 0.0f), BUSINESS_TYPE_BAR, 0.0f),
 
            // Clubhouse
            new BusinessItemModel(Messages.ITM_BEER_BOTTLE, ITEM_HASH_BOTTLE_BEER_PISSWASSER, ITEM_TYPE_CONSUMABLE, 10, 0.1f, 1, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(270.0f, 0.0f, 0.0f), BUSINESS_TYPE_CLUBHOUSE, 0.08f),
            new BusinessItemModel(Messages.ITM_BURGER, ITEM_HASH_BURGER, ITEM_TYPE_CONSUMABLE, 5, 0.1f, 20, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_CLUBHOUSE, 0.0f),
            new BusinessItemModel(Messages.ITM_COFFEE, ITEM_HASH_CUP_COFFEE, ITEM_TYPE_CONSUMABLE, 10, 0.1f, 5, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(270.0f, 0.0f, 0.0f), BUSINESS_TYPE_CLUBHOUSE, 0.0f),
            new BusinessItemModel(Messages.ITM_COLA, ITEM_HASH_CAN_COLA, ITEM_TYPE_CONSUMABLE, 5, 0.1f, 5, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(270.0f, 0.0f, 0.0f), BUSINESS_TYPE_CLUBHOUSE, 0.0f),
            new BusinessItemModel(Messages.ITM_HOTDOG, ITEM_HASH_HOTDOG, ITEM_TYPE_CONSUMABLE, 2, 0.1f, 15, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(270.0f, 0.0f, 0.0f), BUSINESS_TYPE_CLUBHOUSE, 0.0f),
 
            // Disco
            new BusinessItemModel(Messages.ITM_BEER_BOTTLE, ITEM_HASH_BOTTLE_BEER_AM, ITEM_TYPE_CONSUMABLE, 10, 0.1f, 1, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(270.0f, 0.0f, 0.0f), BUSINESS_TYPE_DISCO, 0.08f),
            new BusinessItemModel(Messages.ITM_JUICE, ITEM_HASH_CUP_JUICE, ITEM_TYPE_CONSUMABLE, 6, 0.1f, 10, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_DISCO, 0.0f),
            new BusinessItemModel(Messages.ITM_ENERGY_DRINK, ITEM_HASH_ENERGY_DRINK, ITEM_TYPE_CONSUMABLE, 6, 0.1f, 5, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_DISCO, 0.0f),
            new BusinessItemModel(Messages.ITM_CAVA_BOTTLE, ITEM_HASH_BOTTLE_CAVA, ITEM_TYPE_CONSUMABLE, 70, 0.1f, 15, 5, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_DISCO, 0.05f),
 
            // Ammu-Nation
            new BusinessItemModel(Messages.ITM_PISTOL, WeaponHash.Pistol.ToString(), ITEM_TYPE_WEAPON, 2000, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_AMMUNATION, 0.0f),
            new BusinessItemModel(Messages.ITM_PISTOL_AMMO, ITEM_HASH_PISTOL_AMMO_CLIP, ITEM_TYPE_AMMUNITION, 300, 0.1f, 0, STACK_PISTOL_CAPACITY, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_AMMUNATION, 0.0f),
            new BusinessItemModel(Messages.ITM_SMG_AMMO, ITEM_HASH_MACHINEGUN_AMMO_CLIP, ITEM_TYPE_AMMUNITION, 500, 0.1f, 0, STACK_MACHINEGUN_CAPACITY, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_AMMUNATION, 0.0f),
            new BusinessItemModel(Messages.ITM_SHOTGUN_AMMO, ITEM_HASH_SHOTGUN_AMMO_CLIP, ITEM_TYPE_AMMUNITION, 400, 0.1f, 0, STACK_SHOTGUN_CAPACITY, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_AMMUNATION, 0.0f),
            new BusinessItemModel(Messages.ITM_RIFLE_AMMO, ITEM_HASH_ASSAULTRIFLE_AMMO_CLIP, ITEM_TYPE_AMMUNITION, 1000, 0.1f, 0, STACK_ASSAULTRIFLE_CAPACITY, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_AMMUNATION, 0.0f),
            new BusinessItemModel(Messages.ITM_SNIPER_AMMO, ITEM_HASH_SNIPERRIFLE_AMMO_CLIP, ITEM_TYPE_AMMUNITION, 2000, 0.1f, 0, STACK_SNIPERRIFLE_CAPACITY, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_AMMUNATION, 0.0f),

            // Clothes store
            new BusinessItemModel(Messages.ITM_BAT, WeaponHash.Bat.ToString(), ITEM_TYPE_WEAPON, 300, 0.1f, 0, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_CLOTHES, 0.0f),
            new BusinessItemModel(Messages.ITM_GOLF_CLUB, WeaponHash.GolfClub.ToString(), ITEM_TYPE_WEAPON, 250, 0.1f, 0, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_CLOTHES, 0.0f),
 
            // Fishing store
            new BusinessItemModel(Messages.ITM_FISHING_ROD, ITEM_HASH_FISHING_ROD, ITEM_TYPE_EQUIPABLE, 250, 0.1f, 0, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_FISHING, 0.0f),
            new BusinessItemModel(Messages.ITM_BAIT, ITEM_HASH_BAIT, ITEM_TYPE_MISC, 10, 0.1f, 0, 10, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_FISHING, 0.0f),
 
            // Miscellaneous
            new BusinessItemModel(Messages.ITM_FISH, ITEM_HASH_FISH, ITEM_TYPE_MISC, 0, 0.1f, 0, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(Messages.ITM_PRODUCTS, ITEM_HASH_BUSINESS_PRODUCTS, ITEM_TYPE_MISC, 50, 0.1f, 0, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(Messages.ITM_STOLEN_ITEMS, ITEM_HASH_STOLEN_OBJECTS, ITEM_TYPE_MISC, 50, 0.1f, 0, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(Messages.ITM_BULLPUP_SHOTGUN, WeaponHash.BullpupShotgun.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(Messages.ITM_COMPACT_RIFLE, WeaponHash.CompactRifle.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(Messages.ITM_CARBINE_RIFLE, WeaponHash.CarbineRifle.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(Messages.ITM_HEAVY_SHOTGUN, WeaponHash.HeavyShotgun.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(Messages.ITM_SAWN_OFF_SHOTGUN, WeaponHash.SawnOffShotgun.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(Messages.ITM_BULLPUP_RIFLE, WeaponHash.BullpupRifle.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(Messages.ITM_ASSAULT_RIFLE, WeaponHash.AssaultRifle.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(Messages.ITM_AP_PISTOL, WeaponHash.APPistol.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(Messages.ITM_DOUBLE_BARREL_SHOTGUN, WeaponHash.DoubleBarrelShotgun.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(Messages.ITM_MACHINE_PISTOL, WeaponHash.MachinePistol.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(Messages.ITM_SNIPER_RIFLE, WeaponHash.SniperRifle.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(Messages.ITM_ASSAULT_SMG, WeaponHash.AssaultSMG.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(Messages.ITM_COMBAT_PDW, WeaponHash.CombatPDW.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(Messages.ITM_REVOLVER, WeaponHash.Revolver.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(Messages.ITM_HEAVY_PISTOL, WeaponHash.HeavyPistol.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(Messages.ITM_PUMP_SHOTGUN, WeaponHash.PumpShotgun.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(Messages.ITM_SPECIAL_CARBINE, WeaponHash.SpecialCarbine.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(Messages.ITM_PISTOL_50, WeaponHash.Pistol50.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(Messages.ITM_ADVANCED_RIFLE, WeaponHash.AdvancedRifle.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(Messages.ITM_HEAVY_SNIPER, WeaponHash.HeavySniper.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(Messages.ITM_MICRO_SMG, WeaponHash.MicroSMG.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(Messages.ITM_ASSAULT_SHOTGUN, WeaponHash.AssaultShotgun.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(Messages.ITM_MARKSMAN_RIFLE, WeaponHash.MarksmanRifle.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(Messages.ITM_SMG, WeaponHash.SMG.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(Messages.ITM_PISTOL_AMMO, ITEM_HASH_PISTOL_AMMO_CLIP, ITEM_TYPE_AMMUNITION, 0, 0.1f, 0, STACK_PISTOL_CAPACITY, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(Messages.ITM_SMG_AMMO, ITEM_HASH_MACHINEGUN_AMMO_CLIP, ITEM_TYPE_AMMUNITION, 0, 0.1f, 0, STACK_MACHINEGUN_CAPACITY, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(Messages.ITM_SHOTGUN_AMMO, ITEM_HASH_SHOTGUN_AMMO_CLIP, ITEM_TYPE_AMMUNITION, 0, 0.1f, 0, STACK_SHOTGUN_CAPACITY, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(Messages.ITM_RIFLE_AMMO, ITEM_HASH_ASSAULTRIFLE_AMMO_CLIP, ITEM_TYPE_AMMUNITION, 0, 0.1f, 0, STACK_ASSAULTRIFLE_CAPACITY, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(Messages.ITM_SNIPER_AMMO, ITEM_HASH_SNIPERRIFLE_AMMO_CLIP, ITEM_TYPE_AMMUNITION, 0, 0.1f, 0, STACK_SNIPERRIFLE_CAPACITY, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f)
        };

        // Clothes list
        public static List<BusinessClothesModel> BUSINESS_CLOTHES_LIST = new List<BusinessClothesModel>
        {
            // Masks
            new BusinessClothesModel(0, "Cerdo", CLOTHES_MASK, 1, SEX_NONE, 0),
            new BusinessClothesModel(0, "Calavera", CLOTHES_MASK, 2, SEX_NONE, 0),
            new BusinessClothesModel(0, "Mono fumador", CLOTHES_MASK, 3, SEX_NONE, 0),
            new BusinessClothesModel(0, "Hockey", CLOTHES_MASK, 4, SEX_NONE, 0),
            new BusinessClothesModel(0, "Mono feliz", CLOTHES_MASK, 5, SEX_NONE, 0),
            new BusinessClothesModel(0, "Cosa siniestra", CLOTHES_MASK, 6, SEX_NONE, 0),
            new BusinessClothesModel(0, "Gárgola", CLOTHES_MASK, 7, SEX_NONE, 0),
            new BusinessClothesModel(0, "Santa", CLOTHES_MASK, 8, SEX_NONE, 0),
            new BusinessClothesModel(0, "Reno", CLOTHES_MASK, 9, SEX_NONE, 0),
            new BusinessClothesModel(0, "Frosty", CLOTHES_MASK, 10, SEX_NONE, 0),
            new BusinessClothesModel(0, "Antifaz", CLOTHES_MASK, 11, SEX_NONE, 0),
            new BusinessClothesModel(0, "Pinocho veneciano", CLOTHES_MASK, 12, SEX_NONE, 0),
            new BusinessClothesModel(0, "Cupido", CLOTHES_MASK, 13, SEX_NONE, 0),
            new BusinessClothesModel(0, "Balística", CLOTHES_MASK, 14, SEX_NONE, 0),
            new BusinessClothesModel(0, "Hockey calavera", CLOTHES_MASK, 15, SEX_NONE, 0),
            new BusinessClothesModel(0, "Hannibal Lecter", CLOTHES_MASK, 16, SEX_NONE, 0),
            new BusinessClothesModel(0, "Gato", CLOTHES_MASK, 17, SEX_NONE, 0),
            new BusinessClothesModel(0, "Zorro", CLOTHES_MASK, 18, SEX_NONE, 0),
            new BusinessClothesModel(0, "Búho", CLOTHES_MASK, 19, SEX_NONE, 0),
            new BusinessClothesModel(0, "Tejón", CLOTHES_MASK, 20, SEX_NONE, 0),
            new BusinessClothesModel(0, "Oso", CLOTHES_MASK, 21, SEX_NONE, 0),
            new BusinessClothesModel(0, "Bisonte", CLOTHES_MASK, 22, SEX_NONE, 0),
            new BusinessClothesModel(0, "Toro", CLOTHES_MASK, 23, SEX_NONE, 0),
            new BusinessClothesModel(0, "Águila", CLOTHES_MASK, 24, SEX_NONE, 0),
            new BusinessClothesModel(0, "Grulla turbia", CLOTHES_MASK, 25, SEX_NONE, 0),
            new BusinessClothesModel(0, "Lobo", CLOTHES_MASK, 26, SEX_NONE, 0),
            new BusinessClothesModel(0, "Gorro de aviador", CLOTHES_MASK, 27, SEX_NONE, 0),
            new BusinessClothesModel(0, "Calavera negra", CLOTHES_MASK, 29, SEX_NONE, 0),
            new BusinessClothesModel(0, "Hockey Jason", CLOTHES_MASK, 30, SEX_NONE, 0),
            new BusinessClothesModel(0, "Pingüino", CLOTHES_MASK, 31, SEX_NONE, 0),
            new BusinessClothesModel(0, "Media roja", CLOTHES_MASK, 32, SEX_NONE, 0),
            new BusinessClothesModel(0, "Jengibre feliz", CLOTHES_MASK, 33, SEX_NONE, 0),
            new BusinessClothesModel(0, "Duende", CLOTHES_MASK, 34, SEX_NONE, 0),
            new BusinessClothesModel(0, "Pasamontañas", CLOTHES_MASK, 35, SEX_NONE, 0),
            new BusinessClothesModel(0, "Media negra", CLOTHES_MASK, 37, SEX_NONE, 0),
            new BusinessClothesModel(0, "Zombie", CLOTHES_MASK, 39, SEX_NONE, 0),
            new BusinessClothesModel(0, "Momia", CLOTHES_MASK, 40, SEX_NONE, 0),
            new BusinessClothesModel(0, "Vampiro", CLOTHES_MASK, 41, SEX_NONE, 0),
            new BusinessClothesModel(0, "Reconstruído", CLOTHES_MASK, 42, SEX_NONE, 0),
            new BusinessClothesModel(0, "Superhéroe", CLOTHES_MASK, 43, SEX_NONE, 0),
            new BusinessClothesModel(0, "Waifu", CLOTHES_MASK, 44, SEX_NONE, 0),
            new BusinessClothesModel(0, "Detective", CLOTHES_MASK, 45, SEX_NONE, 0),
            new BusinessClothesModel(0, "Cinta policial", CLOTHES_MASK, 47, SEX_NONE, 0),
            new BusinessClothesModel(0, "Cinta", CLOTHES_MASK, 48, SEX_NONE, 0),
            new BusinessClothesModel(0, "Bolsa", CLOTHES_MASK, 49, SEX_NONE, 0),
            new BusinessClothesModel(0, "Estatua", CLOTHES_MASK, 50, SEX_NONE, 0),
            new BusinessClothesModel(0, "Bandana", CLOTHES_MASK, 51, SEX_NONE, 0),
            new BusinessClothesModel(0, "Capucha", CLOTHES_MASK, 53, SEX_NONE, 0),
            new BusinessClothesModel(0, "Camiseta", CLOTHES_MASK, 54, SEX_NONE, 0),
            new BusinessClothesModel(0, "Gorro", CLOTHES_MASK, 55, SEX_NONE, 0),
            new BusinessClothesModel(0, "Pasamontañas azul", CLOTHES_MASK, 56, SEX_NONE, 0),
            new BusinessClothesModel(0, "Pasamontañas lana", CLOTHES_MASK, 57, SEX_NONE, 0),
            new BusinessClothesModel(0, "Pasamontañas rallado", CLOTHES_MASK, 58, SEX_NONE, 0),
            new BusinessClothesModel(0, "Hombre lobo", CLOTHES_MASK, 59, SEX_NONE, 0),
            new BusinessClothesModel(0, "Calabaza maligna", CLOTHES_MASK, 60, SEX_NONE, 0),
            new BusinessClothesModel(0, "Viejo zombie", CLOTHES_MASK, 61, SEX_NONE, 0),
            new BusinessClothesModel(0, "Freddy Krueger", CLOTHES_MASK, 62, SEX_NONE, 0),
            new BusinessClothesModel(0, "Shingeki no kyojin", CLOTHES_MASK, 63, SEX_NONE, 0),
            new BusinessClothesModel(0, "Calavera vomitada", CLOTHES_MASK, 64, SEX_NONE, 0),
            new BusinessClothesModel(0, "Perro lobo cabreado", CLOTHES_MASK, 65, SEX_NONE, 0),
            new BusinessClothesModel(0, "Moscardon con lengua", CLOTHES_MASK, 66, SEX_NONE, 0),
            new BusinessClothesModel(0, "Orco de mordor", CLOTHES_MASK, 67, SEX_NONE, 0),
            new BusinessClothesModel(0, "Demonio con cuernos", CLOTHES_MASK, 68, SEX_NONE, 0),
            new BusinessClothesModel(0, "Hombre del saco", CLOTHES_MASK, 69, SEX_NONE, 0),
            new BusinessClothesModel(0, "Calavera mexicana zombie", CLOTHES_MASK, 70, SEX_NONE, 0),
            new BusinessClothesModel(0, "Bruja piruja", CLOTHES_MASK, 71, SEX_NONE, 0),
            new BusinessClothesModel(0, "Demonio con cuernos bronceado", CLOTHES_MASK, 72, SEX_NONE, 0),
            new BusinessClothesModel(0, "Sin pelo", CLOTHES_MASK, 73, SEX_NONE, 0),
            new BusinessClothesModel(0, "Jengibre enfadado bronceado", CLOTHES_MASK, 74, SEX_NONE, 0),
            new BusinessClothesModel(0, "Jengibre enfadado", CLOTHES_MASK, 75, SEX_NONE, 0),
            new BusinessClothesModel(0, "Papa noel grumete", CLOTHES_MASK, 76, SEX_NONE, 0),
            new BusinessClothesModel(0, "Arbol de navidad cutre", CLOTHES_MASK, 77, SEX_NONE, 0),
            new BusinessClothesModel(0, "Bizcocho de chocolate con crema pastelera", CLOTHES_MASK, 78, SEX_NONE, 0),
            new BusinessClothesModel(0, "Otro hombre lobo muy peludo", CLOTHES_MASK, 79, SEX_NONE, 0),
            new BusinessClothesModel(0, "Hombre lobo con gorra LS", CLOTHES_MASK, 80, SEX_NONE, 0),
            new BusinessClothesModel(0, "Hombre lobo listo para jugar al tenis", CLOTHES_MASK, 81, SEX_NONE, 0),
            new BusinessClothesModel(0, "Hombre lobo gym", CLOTHES_MASK, 82, SEX_NONE, 0),
            new BusinessClothesModel(0, "Hombre lobo os desea feliz navidad", CLOTHES_MASK, 83, SEX_NONE, 0),
            new BusinessClothesModel(0, "Yeti de las nieves aburrido", CLOTHES_MASK, 84, SEX_NONE, 0),
            new BusinessClothesModel(0, "Pollo relleno de cara", CLOTHES_MASK, 85, SEX_NONE, 0),
            new BusinessClothesModel(0, "Vieja muy muy pasada de todo", CLOTHES_MASK, 86, SEX_NONE, 0),
            new BusinessClothesModel(0, "Abuelo con mala leche", CLOTHES_MASK, 87, SEX_NONE, 0),
            new BusinessClothesModel(0, "Vieja despues de proyecto hombre", CLOTHES_MASK, 88, SEX_NONE, 0),
            new BusinessClothesModel(0, "Tipo motorista negra", CLOTHES_MASK, 89, SEX_NONE, 0),
            new BusinessClothesModel(0, "Media cara nariz boca roja", CLOTHES_MASK, 90, SEX_NONE, 0),
            new BusinessClothesModel(0, "Casco del espacio", CLOTHES_MASK, 91, SEX_NONE, 0),
            new BusinessClothesModel(0, "Cthulhu adolescente", CLOTHES_MASK, 92, SEX_NONE, 0),
            new BusinessClothesModel(0, "T-REX", CLOTHES_MASK, 93, SEX_NONE, 0),
            new BusinessClothesModel(0, "Oni, demonio japones", CLOTHES_MASK, 94, SEX_NONE, 0),
            new BusinessClothesModel(0, "Payaso sin gracia", CLOTHES_MASK, 95, SEX_NONE, 0),
            new BusinessClothesModel(0, "King Kong", CLOTHES_MASK, 96, SEX_NONE, 0),
            new BusinessClothesModel(0, "Caballo", CLOTHES_MASK, 97, SEX_NONE, 0),
            new BusinessClothesModel(0, "Unicornio", CLOTHES_MASK, 98, SEX_NONE, 0),
            new BusinessClothesModel(0, "Calavera roja con trazos dorados", CLOTHES_MASK, 99, SEX_NONE, 0),
            new BusinessClothesModel(0, "PUG", CLOTHES_MASK, 100, SEX_NONE, 0),
            new BusinessClothesModel(0, "BIGNESS media cara nariz boca", CLOTHES_MASK, 101, SEX_NONE, 0),
            new BusinessClothesModel(0, "Dibujado por niños", CLOTHES_MASK, 102, SEX_NONE, 0),

            // Female pants
            new BusinessClothesModel(0, "vaqueros estrechos oscuros", CLOTHES_LEGS, 0, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "vaqueros anchos oscuros", CLOTHES_LEGS, 1, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "deportivo recogido blanco", CLOTHES_LEGS, 2, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "normal negro cinturon blanco", CLOTHES_LEGS, 3, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "vaquero pirata oscuro", CLOTHES_LEGS, 4, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "corto a cuadros", CLOTHES_LEGS, 5, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "normal negro", CLOTHES_LEGS, 6, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "falda negra", CLOTHES_LEGS, 7, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "minifalda negra", CLOTHES_LEGS, 8, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "minifalda negra con motivos", CLOTHES_LEGS, 9, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "deportivo corto blanco y celeste", CLOTHES_LEGS, 10, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "vaquero pirata marron", CLOTHES_LEGS, 11, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "minifalda volantes a cuadros", CLOTHES_LEGS, 12, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "corto gris", CLOTHES_LEGS, 14, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "bikini negro", CLOTHES_LEGS, 15, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "corto amarillo", CLOTHES_LEGS, 16, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "bikini blanco", CLOTHES_LEGS, 17, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "falda roja", CLOTHES_LEGS, 18, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "interior encaje blanca", CLOTHES_LEGS, 19, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "interior encaje blanca con liguero y media", CLOTHES_LEGS, 20, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "bikini rojo", CLOTHES_LEGS, 21, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "recto blanco", CLOTHES_LEGS, 23, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "falda comic", CLOTHES_LEGS, 24, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "corto vaquero", CLOTHES_LEGS, 25, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "minifalda leopardo", CLOTHES_LEGS, 26, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "latex negro", CLOTHES_LEGS, 27, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "minifalda rayas roja y blanca", CLOTHES_LEGS, 28, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "ancho con agarres marron", CLOTHES_LEGS, 29, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "normal pirata negro", CLOTHES_LEGS, 30, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "leggins rojo", CLOTHES_LEGS, 31, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "ancho negro con rodilleras", CLOTHES_LEGS, 33, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "normal negro hebilla dorada", CLOTHES_LEGS, 34, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "ancho verde con reflectante", CLOTHES_LEGS, 35, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "falda gris", CLOTHES_LEGS, 36, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "normal negro", CLOTHES_LEGS, 37, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "ancho rojo", CLOTHES_LEGS, 38, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "ancho marron cinturon oscuro", CLOTHES_LEGS, 41, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "ancho con agarres negro", CLOTHES_LEGS, 42, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "vaquero negro roto frontal", CLOTHES_LEGS, 43, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "vaquero negro roto lateral", CLOTHES_LEGS, 44, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "ancho verde militar", CLOTHES_LEGS, 45, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "normal negro", CLOTHES_LEGS, 47, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "ancho marron rodilleras cinturon negro", CLOTHES_LEGS, 48, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "normal marron cinturon negro", CLOTHES_LEGS, 49, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "recto blanco", CLOTHES_LEGS, 50, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "leggins blanco", CLOTHES_LEGS, 51, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "recto rojo oscuro", CLOTHES_LEGS, 52, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "normal oscuro con flores marrones", CLOTHES_LEGS, 53, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "leggins rojo oscuro", CLOTHES_LEGS, 54, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "leggins oscuro con flores marrones", CLOTHES_LEGS, 55, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "bikini rosa", CLOTHES_LEGS, 56, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "falda blanca con lazo", CLOTHES_LEGS, 57, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "chandal negro rayas blancas lateral", CLOTHES_LEGS, 58, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "normal a cuadros rojo", CLOTHES_LEGS, 60, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "culotte encaje blanco y negro", CLOTHES_LEGS, 62, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "interior blanco y legro con liguero y medias", CLOTHES_LEGS, 63, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "normal camel", CLOTHES_LEGS, 64, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "chandal azul rayas blancas lateral", CLOTHES_LEGS, 66, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "normal blanco con rayas grises", CLOTHES_LEGS, 67, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "chandal negro y blanco", CLOTHES_LEGS, 68, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "ancho negro con calaveras blancas", CLOTHES_LEGS, 71, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "vaquero pegado oscuro", CLOTHES_LEGS, 73, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "vaquero rasgado oscuro", CLOTHES_LEGS, 74, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "vaquero pegado negro", CLOTHES_LEGS, 75, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "short vaquero con medias oscuras", CLOTHES_LEGS, 78, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "chandal marron oscuro", CLOTHES_LEGS, 80, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "chandal negro", CLOTHES_LEGS, 81, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "chandal pirata marron oscuro", CLOTHES_LEGS, 82, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "chandal pirata negro", CLOTHES_LEGS, 83, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "vaquero bajos negro", CLOTHES_LEGS, 84, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "normal bajos negro", CLOTHES_LEGS, 85, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "leggins militar oscuro y rojo", CLOTHES_LEGS, 87, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "leggins rayas colores fosforitos", CLOTHES_LEGS, 88, SEX_FEMALE, 0),

            // Male pants
            new BusinessClothesModel(0, "vaquero azul oscuro cintu negro", CLOTHES_LEGS, 0, SEX_MALE, 0),
            new BusinessClothesModel(0, "vaquero bajo slip visible", CLOTHES_LEGS, 1, SEX_MALE, 0),
            new BusinessClothesModel(0, "corto cuadros blanco y negro", CLOTHES_LEGS, 2, SEX_MALE, 0),
            new BusinessClothesModel(0, "chandal blanco", CLOTHES_LEGS, 3, SEX_MALE, 0),
            new BusinessClothesModel(0, "vaquero pitillo negro", CLOTHES_LEGS, 4, SEX_MALE, 0),
            new BusinessClothesModel(0, "chandal ancho blanco", CLOTHES_LEGS, 5, SEX_MALE, 0),
            new BusinessClothesModel(0, "corto blanco", CLOTHES_LEGS, 6, SEX_MALE, 0),
            new BusinessClothesModel(0, "normal negro cintu negro", CLOTHES_LEGS, 7, SEX_MALE, 0),
            new BusinessClothesModel(0, "ancho gris oscuro", CLOTHES_LEGS, 8, SEX_MALE, 0),
            new BusinessClothesModel(0, "ancho verde oscuro cinturon", CLOTHES_LEGS, 9, SEX_MALE, 0),
            new BusinessClothesModel(0, "normal negro cinturon", CLOTHES_LEGS, 10, SEX_MALE, 0),
            new BusinessClothesModel(0, "corto negro", CLOTHES_LEGS, 12, SEX_MALE, 0),
            new BusinessClothesModel(0, "normal negro cinturon", CLOTHES_LEGS, 13, SEX_MALE, 0),
            new BusinessClothesModel(0, "calzonas gris y blanco", CLOTHES_LEGS, 14, SEX_MALE, 0),
            new BusinessClothesModel(0, "corto marron claro", CLOTHES_LEGS, 15, SEX_MALE, 0),
            new BusinessClothesModel(0, "corto rosa y azul", CLOTHES_LEGS, 16, SEX_MALE, 0),
            new BusinessClothesModel(0, "corto marron oscuro", CLOTHES_LEGS, 17, SEX_MALE, 0),
            new BusinessClothesModel(0, "calzonas amarillo", CLOTHES_LEGS, 18, SEX_MALE, 0),
            new BusinessClothesModel(0, "normal rojo con cinturon negro", CLOTHES_LEGS, 19, SEX_MALE, 0),
            new BusinessClothesModel(0, "normal blanco cinturon negro", CLOTHES_LEGS, 20, SEX_MALE, 0),
            new BusinessClothesModel(0, "slip negro corazones rojos", CLOTHES_LEGS, 21, SEX_MALE, 0),
            new BusinessClothesModel(0, "normal gris", CLOTHES_LEGS, 22, SEX_MALE, 0),
            new BusinessClothesModel(0, "pitillo negro", CLOTHES_LEGS, 24, SEX_MALE, 0),
            new BusinessClothesModel(0, "normal negro hebilla negra", CLOTHES_LEGS, 25, SEX_MALE, 0),
            new BusinessClothesModel(0, "cuero negro", CLOTHES_LEGS, 26, SEX_MALE, 0),
            new BusinessClothesModel(0, "ancho amarillo", CLOTHES_LEGS, 27, SEX_MALE, 0),
            new BusinessClothesModel(0, "ancho rayas rojas y blancas", CLOTHES_LEGS, 29, SEX_MALE, 0),
            new BusinessClothesModel(0, "ancho verde oscuro agarres", CLOTHES_LEGS, 30, SEX_MALE, 0),
            new BusinessClothesModel(0, "pitillo negro ancho", CLOTHES_LEGS, 31, SEX_MALE, 0),
            new BusinessClothesModel(0, "ceñido rojo", CLOTHES_LEGS, 32, SEX_MALE, 0),
            new BusinessClothesModel(0, "ancho negro rodilleras", CLOTHES_LEGS, 34, SEX_MALE, 0),
            new BusinessClothesModel(0, "ancho verde oscuro reflectante", CLOTHES_LEGS, 36, SEX_MALE, 0),
            new BusinessClothesModel(0, "normal marron cinturon oscuro", CLOTHES_LEGS, 37, SEX_MALE, 0),
            new BusinessClothesModel(0, "ancho rojo oscuro", CLOTHES_LEGS, 38, SEX_MALE, 0),
            new BusinessClothesModel(0, "ancho negro agarres", CLOTHES_LEGS, 41, SEX_MALE, 0),
            new BusinessClothesModel(0, "chandal corto negro", CLOTHES_LEGS, 42, SEX_MALE, 0),
            new BusinessClothesModel(0, "vaquero normal oscuro", CLOTHES_LEGS, 43, SEX_MALE, 0),
            new BusinessClothesModel(0, "chandal ajustado negro", CLOTHES_LEGS, 45, SEX_MALE, 0),
            new BusinessClothesModel(0, "ancho marron rodilleras", CLOTHES_LEGS, 46, SEX_MALE, 0),
            new BusinessClothesModel(0, "ancho marron cinturon negro", CLOTHES_LEGS, 47, SEX_MALE, 0),
            new BusinessClothesModel(0, "normal marron claro cinturon negro", CLOTHES_LEGS, 48, SEX_MALE, 0),
            new BusinessClothesModel(0, "pitillo marron claro cinturon negro", CLOTHES_LEGS, 49, SEX_MALE, 0),
            new BusinessClothesModel(0, "normal rojo oscuro cinturon negro", CLOTHES_LEGS, 50, SEX_MALE, 0),
            new BusinessClothesModel(0, "normal oscuro flores marron", CLOTHES_LEGS, 51, SEX_MALE, 0),
            new BusinessClothesModel(0, "pitillo rojo oscuro cinturon negro", CLOTHES_LEGS, 52, SEX_MALE, 0),
            new BusinessClothesModel(0, "pitillo oscuro flores marron", CLOTHES_LEGS, 53, SEX_MALE, 0),
            new BusinessClothesModel(0, "corto oscuro rayas azul oscuro", CLOTHES_LEGS, 54, SEX_MALE, 0),
            new BusinessClothesModel(0, "chandal oscuro rayas blancas", CLOTHES_LEGS, 55, SEX_MALE, 0),
            new BusinessClothesModel(0, "falda blanca lazo", CLOTHES_LEGS, 56, SEX_MALE, 0),
            new BusinessClothesModel(0, "normal a cuadros rojo", CLOTHES_LEGS, 58, SEX_MALE, 0),
            new BusinessClothesModel(0, "normal rayas azul oscuro y negro", CLOTHES_LEGS, 60, SEX_MALE, 0),
            new BusinessClothesModel(0, "calzonas blancas", CLOTHES_LEGS, 61, SEX_MALE, 0),
            new BusinessClothesModel(0, "pirata negro cinturon amarillo", CLOTHES_LEGS, 62, SEX_MALE, 0),
            new BusinessClothesModel(0, "vaquero ancho oscuro", CLOTHES_LEGS, 63, SEX_MALE, 0),
            new BusinessClothesModel(0, "chandal ancho azul rayas blancas", CLOTHES_LEGS, 64, SEX_MALE, 0),
            new BusinessClothesModel(0, "normal blanco a rayas", CLOTHES_LEGS, 65, SEX_MALE, 0),
            new BusinessClothesModel(0, "chandal negro y blanco", CLOTHES_LEGS, 66, SEX_MALE, 0),
            new BusinessClothesModel(0, "ancho negro con calaveras blancas", CLOTHES_LEGS, 69, SEX_MALE, 0),
            new BusinessClothesModel(0, "cuero negro", CLOTHES_LEGS, 71, SEX_MALE, 0),
            new BusinessClothesModel(0, "vaquero ancho oscuro cinturon negro", CLOTHES_LEGS, 75, SEX_MALE, 0),
            new BusinessClothesModel(0, "vaquero rasgado", CLOTHES_LEGS, 76, SEX_MALE, 0),
            new BusinessClothesModel(0, "pitillo con motivos fosforito", CLOTHES_LEGS, 77, SEX_MALE, 0),
            new BusinessClothesModel(0, "chandal rojo oscuro", CLOTHES_LEGS, 78, SEX_MALE, 0),
            new BusinessClothesModel(0, "chandal pitillo negro", CLOTHES_LEGS, 79, SEX_MALE, 0),
            new BusinessClothesModel(0, "pirata marron oscuro", CLOTHES_LEGS, 80, SEX_MALE, 0),
            new BusinessClothesModel(0, "pirata negro", CLOTHES_LEGS, 81, SEX_MALE, 0),
            new BusinessClothesModel(0, "vaquero oscuro pitillo", CLOTHES_LEGS, 82, SEX_MALE, 0),
            new BusinessClothesModel(0, "pitillo negro", CLOTHES_LEGS, 83, SEX_MALE, 0),
            new BusinessClothesModel(0, "ajustado con motivos coloridos", CLOTHES_LEGS, 85, SEX_MALE, 0),

            // Bags
            new BusinessClothesModel(0, "Gris, blanca y negra con rayas azules", CLOTHES_BAGS, 1, SEX_NONE, 0),
            new BusinessClothesModel(0, "Bandera EEUU", CLOTHES_BAGS, 10, SEX_NONE, 0),
            new BusinessClothesModel(0, "Blanca cruz azul", CLOTHES_BAGS, 21, SEX_NONE, 0),
            new BusinessClothesModel(0, "Negra", CLOTHES_BAGS, 31, SEX_NONE, 0),
            new BusinessClothesModel(0, "Marron grande", CLOTHES_BAGS, 40, SEX_NONE, 0),
            new BusinessClothesModel(0, "Negra grande", CLOTHES_BAGS, 44, SEX_NONE, 0),
            new BusinessClothesModel(0, "Verde y negra", CLOTHES_BAGS, 52, SEX_NONE, 0),

            // Female shoes
            new BusinessClothesModel(0, "Tacon redondo negro", CLOTHES_FEET, 0, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Zapatillas blanca y gris", CLOTHES_FEET, 1, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Botas pelito marron", CLOTHES_FEET, 2, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Converse negra y blanca tobillo bajo", CLOTHES_FEET, 3, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Deportivas gris oscuro y blanco", CLOTHES_FEET, 4, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chanclas negras", CLOTHES_FEET, 5, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Tacon punta negro", CLOTHES_FEET, 6, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Botines negros", CLOTHES_FEET, 7, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Botines abiertos negros", CLOTHES_FEET, 8, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Botas largas negras", CLOTHES_FEET, 9, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Deportivas negro y morado", CLOTHES_FEET, 10, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Zapatillas morado y blanco", CLOTHES_FEET, 11, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Plano negro", CLOTHES_FEET, 13, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Tacon tiras negras", CLOTHES_FEET, 14, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Sandalia negro y plata", CLOTHES_FEET, 15, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chanclas grises", CLOTHES_FEET, 16, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Duende verde", CLOTHES_FEET, 17, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Tacon redondo gris", CLOTHES_FEET, 18, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Tacon redondo marron", CLOTHES_FEET, 19, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Tacon punta leopardo", CLOTHES_FEET, 20, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Botas amarillas", CLOTHES_FEET, 21, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Botines marron", CLOTHES_FEET, 22, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Tacon redondo azul", CLOTHES_FEET, 23, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Botas negras", CLOTHES_FEET, 24, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Botas negras con tachuelas metal", CLOTHES_FEET, 25, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Zapatillas negra", CLOTHES_FEET, 27, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Zapatillas negra marca lateral", CLOTHES_FEET, 28, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Normal punta", CLOTHES_FEET, 29, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Botines negros con hebilla", CLOTHES_FEET, 30, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Zapatillas doradas", CLOTHES_FEET, 31, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Deportivas amarillo, gris, negro y blanco", CLOTHES_FEET, 32, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Converse amarillo y blanco calentadores negros", CLOTHES_FEET, 33, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Descalza", CLOTHES_FEET, 35, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Botas marrones", CLOTHES_FEET, 36, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Normal marron oscuro", CLOTHES_FEET, 37, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Bota cowgirl marron punta", CLOTHES_FEET, 38, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Normal cowgirl marron punta", CLOTHES_FEET, 39, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Tacon redondo marron claro", CLOTHES_FEET, 42, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Botines amarillos", CLOTHES_FEET, 43, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Botines amarillo y negro", CLOTHES_FEET, 44, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Bota cowgirl celeste punta", CLOTHES_FEET, 45, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Normal cowgirl celeste punta", CLOTHES_FEET, 46, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Zapatillas blanca y azul", CLOTHES_FEET, 47, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Botas verde y negro", CLOTHES_FEET, 48, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Converse negra y blanca tobillo alto", CLOTHES_FEET, 49, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Converse amarilla y blanca tobillo alto", CLOTHES_FEET, 50, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Bota marron oscuro hebilla", CLOTHES_FEET, 51, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Normal marron oscuro", CLOTHES_FEET, 52, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Bota marron talon alto", CLOTHES_FEET, 53, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Bota negra tachuelas y cremallera", CLOTHES_FEET, 54, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Normal negro tachuelas", CLOTHES_FEET, 55, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Bota negra hebilla y cremallera", CLOTHES_FEET, 56, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Normal negra hebilla", CLOTHES_FEET, 57, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Deportivas negra con rayas amarillo fosforito", CLOTHES_FEET, 58, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Plano marron", CLOTHES_FEET, 59, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Zapatillas camel", CLOTHES_FEET, 60, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Plano negro rayas rosa y celeste", CLOTHES_FEET, 61, SEX_FEMALE, 0),

            // Male shoes
            new BusinessClothesModel(0, "Zapatillas cuadros negros y blancos tobillo alto", CLOTHES_FEET, 0, SEX_MALE, 0),
            new BusinessClothesModel(0, "Zapatillas negra suela blanca", CLOTHES_FEET, 1, SEX_MALE, 0),
            new BusinessClothesModel(0, "Zapatillas cuadros negros y blancos tobillo bajo", CLOTHES_FEET, 2, SEX_MALE, 0),
            new BusinessClothesModel(0, "Normal marron oscuro y gris", CLOTHES_FEET, 3, SEX_MALE, 0),
            new BusinessClothesModel(0, "Converse azul oscuro tobillo alto", CLOTHES_FEET, 4, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chanclas negra y blanca", CLOTHES_FEET, 5, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chanclas negras calcetines blancos", CLOTHES_FEET, 6, SEX_MALE, 0),
            new BusinessClothesModel(0, "Zapatillas blancas calcetines altos", CLOTHES_FEET, 7, SEX_MALE, 0),
            new BusinessClothesModel(0, "Zapatillas blancas marca calcetines altos", CLOTHES_FEET, 9, SEX_MALE, 0),
            new BusinessClothesModel(0, "Zapatos negros punta calcetines altos", CLOTHES_FEET, 10, SEX_MALE, 0),
            new BusinessClothesModel(0, "Zapatillas planas cuadros negros y blancos", CLOTHES_FEET, 11, SEX_MALE, 0),
            new BusinessClothesModel(0, "Botas marron", CLOTHES_FEET, 12, SEX_MALE, 0),
            new BusinessClothesModel(0, "Botas oscuras suela blanca", CLOTHES_FEET, 14, SEX_MALE, 0),
            new BusinessClothesModel(0, "Bota negra lisa punta", CLOTHES_FEET, 15, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chanclas negra", CLOTHES_FEET, 16, SEX_MALE, 0),
            new BusinessClothesModel(0, "Duende verde", CLOTHES_FEET, 17, SEX_MALE, 0),
            new BusinessClothesModel(0, "Normal blanco y negro calcetin alto", CLOTHES_FEET, 18, SEX_MALE, 0),
            new BusinessClothesModel(0, "Zapato blanco y negro botones negros", CLOTHES_FEET, 19, SEX_MALE, 0),
            new BusinessClothesModel(0, "Zapato marron oscuro punta calcetines altos", CLOTHES_FEET, 20, SEX_MALE, 0),
            new BusinessClothesModel(0, "Zapato marron oscuro punta calcetin bajo", CLOTHES_FEET, 21, SEX_MALE, 0),
            new BusinessClothesModel(0, "Converse azul tobillo alto", CLOTHES_FEET, 22, SEX_MALE, 0),
            new BusinessClothesModel(0, "Normal marron suela amarilla", CLOTHES_FEET, 23, SEX_MALE, 0),
            new BusinessClothesModel(0, "Bota negra", CLOTHES_FEET, 24, SEX_MALE, 0),
            new BusinessClothesModel(0, "Bota negra tachuelas", CLOTHES_FEET, 25, SEX_MALE, 0),
            new BusinessClothesModel(0, "Converse azul oscuro tobillo alto", CLOTHES_FEET, 26, SEX_MALE, 0),
            new BusinessClothesModel(0, "Zapatillas blanca puntos negros tobillo alto", CLOTHES_FEET, 28, SEX_MALE, 0),
            new BusinessClothesModel(0, "Zapatillas dorado tobillo alto", CLOTHES_FEET, 29, SEX_MALE, 0),
            new BusinessClothesModel(0, "Normal blanco y oscuro hebilla", CLOTHES_FEET, 30, SEX_MALE, 0),
            new BusinessClothesModel(0, "Deportivas amarillo gris blanco negro", CLOTHES_FEET, 31, SEX_MALE, 0),
            new BusinessClothesModel(0, "Deportivas negro y blanco", CLOTHES_FEET, 32, SEX_MALE, 0),
            new BusinessClothesModel(0, "Descalzo", CLOTHES_FEET, 34, SEX_MALE, 0),
            new BusinessClothesModel(0, "Bota marron tobillo alto", CLOTHES_FEET, 35, SEX_MALE, 0),
            new BusinessClothesModel(0, "Normal marron oscuro hebilla dorada", CLOTHES_FEET, 36, SEX_MALE, 0),
            new BusinessClothesModel(0, "Bota cowboy alta punta", CLOTHES_FEET, 37, SEX_MALE, 0),
            new BusinessClothesModel(0, "Bota cowboy baja punta", CLOTHES_FEET, 38, SEX_MALE, 0),
            new BusinessClothesModel(0, "Zapato azul y gris calcetin alto", CLOTHES_FEET, 40, SEX_MALE, 0),
            new BusinessClothesModel(0, "Pantufla rojo oscuro y negro", CLOTHES_FEET, 41, SEX_MALE, 0),
            new BusinessClothesModel(0, "Zapatillas grisaceo", CLOTHES_FEET, 42, SEX_MALE, 0),
            new BusinessClothesModel(0, "Bota baja amarillo", CLOTHES_FEET, 43, SEX_MALE, 0),
            new BusinessClothesModel(0, "Bota cowboy alta azul punta", CLOTHES_FEET, 44, SEX_MALE, 0),
            new BusinessClothesModel(0, "Bota cowboy baja azul punta", CLOTHES_FEET, 45, SEX_MALE, 0),
            new BusinessClothesModel(0, "Deportivas blanca y azul", CLOTHES_FEET, 46, SEX_MALE, 0),
            new BusinessClothesModel(0, "Bota alta verde y blanca", CLOTHES_FEET, 47, SEX_MALE, 0),
            new BusinessClothesModel(0, "Converse negro y blanco tobillo alto", CLOTHES_FEET, 48, SEX_MALE, 0),
            new BusinessClothesModel(0, "Converse amarillo y blanco tobillo alto", CLOTHES_FEET, 49, SEX_MALE, 0),
            new BusinessClothesModel(0, "Bota tobillo algo negra", CLOTHES_FEET, 50, SEX_MALE, 0),
            new BusinessClothesModel(0, "Zapatos bajos negro", CLOTHES_FEET, 51, SEX_MALE, 0),
            new BusinessClothesModel(0, "Bota redonda marron", CLOTHES_FEET, 52, SEX_MALE, 0),
            new BusinessClothesModel(0, "Bota negra tobillo alto tachuelas", CLOTHES_FEET, 53, SEX_MALE, 0),
            new BusinessClothesModel(0, "Zapato negro tobillo bajo tachuelas", CLOTHES_FEET, 54, SEX_MALE, 0),
            new BusinessClothesModel(0, "Zapatillas negras rayas amarillo fosforito", CLOTHES_FEET, 55, SEX_MALE, 0),
            new BusinessClothesModel(0, "Zapato marron tobillo bajo suela negra", CLOTHES_FEET, 56, SEX_MALE, 0),
            new BusinessClothesModel(0, "Zapatillas camel", CLOTHES_FEET, 57, SEX_MALE, 0),
            new BusinessClothesModel(0, "Plano negro rayas rojas y celestes", CLOTHES_FEET, 58, SEX_MALE, 0),

            // Female accessories
            new BusinessClothesModel(0, "Pendientes aro ancho dorado", CLOTHES_ACCESSORIES, 1, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Pendientes aro fino dorado", CLOTHES_ACCESSORIES, 2, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Muñequera mano derecha negra", CLOTHES_ACCESSORIES, 3, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Pulseras anchas mano derecha cuadrados negro y blanco", CLOTHES_ACCESSORIES, 4, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Muñequera mano derecha cuadrados negro y blanco", CLOTHES_ACCESSORIES, 5, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Colgante herradura dorada gema interior oscura", CLOTHES_ACCESSORIES, 6, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Colgantes cuerdas negra y dorado con circulo y corazon dorado", CLOTHES_ACCESSORIES, 7, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Palestina negra y blanca", CLOTHES_ACCESSORIES, 9, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Pulseras anchas mano derecha flores", CLOTHES_ACCESSORIES, 10, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Colgantes cuarda negra y dorado con circulo dorado y corazon oscuro", CLOTHES_ACCESSORIES, 11, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Collar perlas blancas", CLOTHES_ACCESSORIES, 12, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Pañuelo negro circulos blancos", CLOTHES_ACCESSORIES, 13, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Muñequera mano derecha tachuelas metal", CLOTHES_ACCESSORIES, 14, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Palestina roja y negra", CLOTHES_ACCESSORIES, 15, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Bufanda buscando a Wally", CLOTHES_ACCESSORIES, 17, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Corbata negra", CLOTHES_ACCESSORIES, 20, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Corbata blanca", CLOTHES_ACCESSORIES, 21, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Corbata negra ajustada", CLOTHES_ACCESSORIES, 22, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Pajarita negra", CLOTHES_ACCESSORIES, 23, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "", CLOTHES_ACCESSORIES, 24, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Cadena oro con colgante G", CLOTHES_ACCESSORIES, 29, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Cadena oro colgante calavera oro ojos rojos", CLOTHES_ACCESSORIES, 30, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Cadena plata colgante calavera plata ojos rojos", CLOTHES_ACCESSORIES, 31, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Cadena oro placa oro", CLOTHES_ACCESSORIES, 32, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Cadena oro € oro", CLOTHES_ACCESSORIES, 33, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Cadena plata colgante plata", CLOTHES_ACCESSORIES, 34, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Cadena oro colgante ancho oro", CLOTHES_ACCESSORIES, 35, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Cadena oro colgante $ oro", CLOTHES_ACCESSORIES, 36, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Cadena oro colgante calavera ojos rojos exterior", CLOTHES_ACCESSORIES, 37, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Cadena plata colgante enmascarado plata exterior", CLOTHES_ACCESSORIES, 38, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Cadena oro colgante placa plata exterior", CLOTHES_ACCESSORIES, 39, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Cadena oro colgante C exterior", CLOTHES_ACCESSORIES, 40, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Cadena oro colgante DIX exterior", CLOTHES_ACCESSORIES, 41, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Cadena oro colgante letras oro exterior", CLOTHES_ACCESSORIES, 42, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Cadena oro claro sin colgante", CLOTHES_ACCESSORIES, 53, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Cadena oro sin colgante", CLOTHES_ACCESSORIES, 54, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Cadena oro ancha", CLOTHES_ACCESSORIES, 55, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Cadena oro claro ancha", CLOTHES_ACCESSORIES, 56, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Palestina marron oscuro y negro", CLOTHES_ACCESSORIES, 83, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Collar perlas marrones", CLOTHES_ACCESSORIES, 84, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Auriculares rojo y blanco", CLOTHES_ACCESSORIES, 85, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Corbata azul y rosa", CLOTHES_ACCESSORIES, 86, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Corbata verde", CLOTHES_ACCESSORIES, 87, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Tirantes negros", CLOTHES_ACCESSORIES, 88, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Cadena oro colgante estrella roja y dorada exterior", CLOTHES_ACCESSORIES, 90, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Cadena oro colgante estrella dorada exterior", CLOTHES_ACCESSORIES, 92, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Collar perlas marron claro", CLOTHES_ACCESSORIES, 93, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Auriculares blanco y azul", CLOTHES_ACCESSORIES, 94, SEX_FEMALE, 0),

            // Male accessories
            new BusinessClothesModel(0, "Corbata blanca ajustada", CLOTHES_ACCESSORIES, 10, SEX_MALE, 0),
            new BusinessClothesModel(0, "Pajarita cuadros blanco y negro", CLOTHES_ACCESSORIES, 11, SEX_MALE, 0),
            new BusinessClothesModel(0, "Corbata blanca", CLOTHES_ACCESSORIES, 12, SEX_MALE, 0),
            new BusinessClothesModel(0, "Cadena plata", CLOTHES_ACCESSORIES, 16, SEX_MALE, 0),
            new BusinessClothesModel(0, "Cadena plata exterior", CLOTHES_ACCESSORIES, 17, SEX_MALE, 0),
            new BusinessClothesModel(0, "Corbata roja ancha", CLOTHES_ACCESSORIES, 18, SEX_MALE, 0),
            new BusinessClothesModel(0, "Corbata roja fina", CLOTHES_ACCESSORIES, 19, SEX_MALE, 0),
            new BusinessClothesModel(0, "Corbata rojo oscuro", CLOTHES_ACCESSORIES, 20, SEX_MALE, 0),
            new BusinessClothesModel(0, "Corbata azul ancha", CLOTHES_ACCESSORIES, 21, SEX_MALE, 0),
            new BusinessClothesModel(0, "Pajarita blanca", CLOTHES_ACCESSORIES, 22, SEX_MALE, 0),
            new BusinessClothesModel(0, "Corbata azul fina", CLOTHES_ACCESSORIES, 23, SEX_MALE, 0),
            new BusinessClothesModel(0, "Corbata blanca ancha", CLOTHES_ACCESSORIES, 24, SEX_MALE, 0),
            new BusinessClothesModel(0, "Corbata blanca fina", CLOTHES_ACCESSORIES, 25, SEX_MALE, 0),
            new BusinessClothesModel(0, "Bufanda blanca", CLOTHES_ACCESSORIES, 30, SEX_MALE, 0),
            new BusinessClothesModel(0, "Pajarita roja blanca y azul", CLOTHES_ACCESSORIES, 32, SEX_MALE, 0),
            new BusinessClothesModel(0, "Bufanda buscando a Wally", CLOTHES_ACCESSORIES, 34, SEX_MALE, 0),
            new BusinessClothesModel(0, "Pajarita negra", CLOTHES_ACCESSORIES, 36, SEX_MALE, 0),
            new BusinessClothesModel(0, "Corbata negra ancha", CLOTHES_ACCESSORIES, 37, SEX_MALE, 0),
            new BusinessClothesModel(0, "Corbata negra ajustada", CLOTHES_ACCESSORIES, 38, SEX_MALE, 0),
            new BusinessClothesModel(0, "Corbata negra suelta", CLOTHES_ACCESSORIES, 39, SEX_MALE, 0),
            new BusinessClothesModel(0, "Cadena oro colgante G", CLOTHES_ACCESSORIES, 42, SEX_MALE, 0),
            new BusinessClothesModel(0, "Cadena oro colgante calavera", CLOTHES_ACCESSORIES, 43, SEX_MALE, 0),
            new BusinessClothesModel(0, "Cadena plata colgante circulo", CLOTHES_ACCESSORIES, 44, SEX_MALE, 0),
            new BusinessClothesModel(0, "Cadena oro colgante placa", CLOTHES_ACCESSORIES, 45, SEX_MALE, 0),
            new BusinessClothesModel(0, "Cadena oro colgante oro claro", CLOTHES_ACCESSORIES, 46, SEX_MALE, 0),
            new BusinessClothesModel(0, "Cadena oro medallon", CLOTHES_ACCESSORIES, 47, SEX_MALE, 0),
            new BusinessClothesModel(0, "Cadena plata colgante enmascarado", CLOTHES_ACCESSORIES, 51, SEX_MALE, 0),
            new BusinessClothesModel(0, "Cadena oro claro", CLOTHES_ACCESSORIES, 74, SEX_MALE, 0),
            new BusinessClothesModel(0, "Cadena oro oscuro", CLOTHES_ACCESSORIES, 75, SEX_MALE, 0),
            new BusinessClothesModel(0, "Cadena oro ancha oscuro", CLOTHES_ACCESSORIES, 76, SEX_MALE, 0),
            new BusinessClothesModel(0, "Cadena grande clara", CLOTHES_ACCESSORIES, 85, SEX_MALE, 0),
            new BusinessClothesModel(0, "Palestina marron y negro", CLOTHES_ACCESSORIES, 112, SEX_MALE, 0),
            new BusinessClothesModel(0, "Collar perlas marron", CLOTHES_ACCESSORIES, 113, SEX_MALE, 0),
            new BusinessClothesModel(0, "Auriculares rojo y blanco", CLOTHES_ACCESSORIES, 114, SEX_MALE, 0),
            new BusinessClothesModel(0, "Corbata azul y rosa", CLOTHES_ACCESSORIES, 115, SEX_MALE, 0),
            new BusinessClothesModel(0, "Corbata verde", CLOTHES_ACCESSORIES, 117, SEX_MALE, 0),
            new BusinessClothesModel(0, "Pajarita gargantilla negro", CLOTHES_ACCESSORIES, 118, SEX_MALE, 0),
            new BusinessClothesModel(0, "Cadena oro colgante estrella rojo", CLOTHES_ACCESSORIES, 119, SEX_MALE, 0),
            new BusinessClothesModel(0, "Cadena oro colgante estrella oro", CLOTHES_ACCESSORIES, 120, SEX_MALE, 0),
            new BusinessClothesModel(0, "Cadena oro colgante estrella rojo exterior", CLOTHES_ACCESSORIES, 121, SEX_MALE, 0),
            new BusinessClothesModel(0, "Cadena oro colgante estrella oro exterior", CLOTHES_ACCESSORIES, 122, SEX_MALE, 0),
            new BusinessClothesModel(0, "Collar perlas marron claro", CLOTHES_ACCESSORIES, 123, SEX_MALE, 0),
            new BusinessClothesModel(0, "Auriculares azul y blanco", CLOTHES_ACCESSORIES, 124, SEX_MALE, 0),

            // Female torsos
            new BusinessClothesModel(0, "Guantes largos morado", CLOTHES_TORSO, 16, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Guantes verde", CLOTHES_TORSO, 17, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Guantes negro", CLOTHES_TORSO, 45, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Guantes negros dedos fuera", CLOTHES_TORSO, 71, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Guantes negros exterior", CLOTHES_TORSO, 31, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Guantes amarillo y blanco", CLOTHES_TORSO, 84, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Guantes blancos", CLOTHES_TORSO, 97, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Guantes celeste", CLOTHES_TORSO, 110, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Guantes verde oscuro", CLOTHES_TORSO, 126, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Guantes verde y blanco moto", CLOTHES_TORSO, 127, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Guantes rosa y blanco moto", CLOTHES_TORSO, 128, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Sin guantes", CLOTHES_TORSO, 0, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Sin guantes 2", CLOTHES_TORSO, 1, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Sin guantes 3", CLOTHES_TORSO, 2, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Sin guantes 4", CLOTHES_TORSO, 3, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Sin guantes 5", CLOTHES_TORSO, 5, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Sin guantes 6", CLOTHES_TORSO, 6, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Sin guantes 7", CLOTHES_TORSO, 7, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Sin guantes 8", CLOTHES_TORSO, 10, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Sin guantes 9", CLOTHES_TORSO, 12, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Sin guantes 10", CLOTHES_TORSO, 13, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Sin guantes 11", CLOTHES_TORSO, 14, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Sin guantes 12", CLOTHES_TORSO, 131, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Sin guantes 13", CLOTHES_TORSO, 161, SEX_FEMALE, 0),
             new BusinessClothesModel(0, "Sin guantes 14", CLOTHES_TORSO, 130, SEX_FEMALE, 0),

            // Male torsos
            new BusinessClothesModel(0, "Guantes verde", CLOTHES_TORSO, 16, SEX_MALE, 0),
            new BusinessClothesModel(0, "Guantes negro", CLOTHES_TORSO, 17, SEX_MALE, 0),
            new BusinessClothesModel(0, "Guantes negro dedos fuera", CLOTHES_TORSO, 18, SEX_MALE, 0),
            new BusinessClothesModel(0, "Guantes negros exterior", CLOTHES_TORSO, 19, SEX_MALE, 0),
            new BusinessClothesModel(0, "Guantes negros basicos", CLOTHES_TORSO, 30, SEX_MALE, 0),
            new BusinessClothesModel(0, "Guantes amarillo y blanco", CLOTHES_TORSO, 63, SEX_MALE, 0),
            new BusinessClothesModel(0, "Guantes blancos", CLOTHES_TORSO, 74, SEX_MALE, 0),
            new BusinessClothesModel(0, "Guantes celestes", CLOTHES_TORSO, 121, SEX_MALE, 0),
            new BusinessClothesModel(0, "Guantes negros altos", CLOTHES_TORSO, 96, SEX_MALE, 0),
            new BusinessClothesModel(0, "Guantes verde oscuro", CLOTHES_TORSO, 99, SEX_MALE, 0),
            new BusinessClothesModel(0, "Guantes verde y blanco moto", CLOTHES_TORSO, 110, SEX_MALE, 0),
            new BusinessClothesModel(0, "Guantes rosa y blanco moto", CLOTHES_TORSO, 111, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sin guantes", CLOTHES_TORSO, 0, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sin Guantes 2", CLOTHES_TORSO, 0, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sin Guantes 3", CLOTHES_TORSO, 1, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sin Guantes 4", CLOTHES_TORSO, 2, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sin Guantes 5", CLOTHES_TORSO, 3, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sin Guantes 6", CLOTHES_TORSO, 4, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sin Guantes 7", CLOTHES_TORSO, 5, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sin Guantes 8", CLOTHES_TORSO, 6, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sin Guantes 9", CLOTHES_TORSO, 7, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sin Guantes 10", CLOTHES_TORSO, 8, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sin Guantes 11", CLOTHES_TORSO, 9, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sin Guantes 12", CLOTHES_TORSO, 10, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sin Guantes 13", CLOTHES_TORSO, 11, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sin Guantes 14", CLOTHES_TORSO, 12, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sin Guantes 15", CLOTHES_TORSO, 13, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sin Guantes 16", CLOTHES_TORSO, 14, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sin Guantes 17", CLOTHES_TORSO, 15, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sin Guantes 18", CLOTHES_TORSO, 112, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sin Guantes 19", CLOTHES_TORSO, 113, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sin Guantes 20", CLOTHES_TORSO, 114, SEX_MALE, 0),

            // Female tops
            new BusinessClothesModel(0, "Camiseta manga corta rojo oscuro y gris", CLOTHES_TOPS, 0, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta vaquera remangada abierta", CLOTHES_TOPS, 1, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta blanca hombro al aire", CLOTHES_TOPS, 2, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Sudadera blanca", CLOTHES_TOPS, 3, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta tirantes cuadros negro y blanco", CLOTHES_TOPS, 4, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Top blanco", CLOTHES_TOPS, 5, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta negra y blanca remangada", CLOTHES_TOPS, 6, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta negra abierta", CLOTHES_TOPS, 7, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta gris remangada abierta", CLOTHES_TOPS, 8, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camisa negra", CLOTHES_TOPS, 9, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Sudadera negra rayas morado y blanco", CLOTHES_TOPS, 10, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta tirantes celeste negra y blanca", CLOTHES_TOPS, 11, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta tirantes cuadros negro y blanco", CLOTHES_TOPS, 12, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Palabra de honor negro", CLOTHES_TOPS, 13, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Polo lacoste blanco", CLOTHES_TOPS, 14, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Bikini negro", CLOTHES_TOPS, 15, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta tirantes roja", CLOTHES_TOPS, 16, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camisa hawaiana", CLOTHES_TOPS, 17, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Bikini blanco", CLOTHES_TOPS, 18, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta mama noel", CLOTHES_TOPS, 19, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta roja y blanca remangada", CLOTHES_TOPS, 20, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Vestido volantes de morado a blanco", CLOTHES_TOPS, 21, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Palabra de honor blanco encaje", CLOTHES_TOPS, 22, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta blanca", CLOTHES_TOPS, 23, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta blanca y negra remangada", CLOTHES_TOPS, 24, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta roja abierta", CLOTHES_TOPS, 25, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta tirantes griega camel", CLOTHES_TOPS, 26, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camisa blanca por dentro de pantalon", CLOTHES_TOPS, 27, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaquetilla gris tirantes", CLOTHES_TOPS, 28, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta azul y negra abierta remangada", CLOTHES_TOPS, 31, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta tirantes leopardo", CLOTHES_TOPS, 32, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Top blanco rayas negras", CLOTHES_TOPS, 33, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta camuflaje abierta", CLOTHES_TOPS, 34, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta corta amarilla y blanca remangada", CLOTHES_TOPS, 35, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta tirantes negra FIST", CLOTHES_TOPS, 36, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Vestido flores rosas", CLOTHES_TOPS, 37, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta verde hombro fuera", CLOTHES_TOPS, 38, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta manga larga EEUU", CLOTHES_TOPS, 39, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta manga larga mama noel", CLOTHES_TOPS, 40, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta azul hombro fuera", CLOTHES_TOPS, 41, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta militar enganches verde", CLOTHES_TOPS, 42, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta manga larga rojo y blanco", CLOTHES_TOPS, 45, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta militar enganches negra", CLOTHES_TOPS, 47, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta gris", CLOTHES_TOPS, 49, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Sudadera negra cuello ancho", CLOTHES_TOPS, 50, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta gris abierta", CLOTHES_TOPS, 52, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta gris remangada", CLOTHES_TOPS, 53, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta marron", CLOTHES_TOPS, 54, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta cuero negro", CLOTHES_TOPS, 55, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camisa negra manga corta", CLOTHES_TOPS, 56, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta negra abierta", CLOTHES_TOPS, 57, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta negra cerrada ", CLOTHES_TOPS, 58, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta rota", CLOTHES_TOPS, 59, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta rota roja y negra manga larga", CLOTHES_TOPS, 60, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta manga larga gris", CLOTHES_TOPS, 61, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta negra con capucha puesta", CLOTHES_TOPS, 62, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta negra con capucha quitada", CLOTHES_TOPS, 63, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta larga veig abierta", CLOTHES_TOPS, 64, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta roja con pelitos en cuello maron", CLOTHES_TOPS, 65, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camisa negra escotada hombreras", CLOTHES_TOPS, 66, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta amarilla hombro fuera", CLOTHES_TOPS, 67, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta tonos amarillos naranja y marron", CLOTHES_TOPS, 68, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta negra con hebillas", CLOTHES_TOPS, 69, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta veig cerrada por cinturon", CLOTHES_TOPS, 70, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camisa cuadritos veig, amarillo, marron y negro remangada", CLOTHES_TOPS, 71, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Sudadera roja y blanca W", CLOTHES_TOPS, 72, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta blanca por dentro del pantalon", CLOTHES_TOPS, 73, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Top blanco poco ajustado", CLOTHES_TOPS, 74, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta amarilla y negra por dentro del pantalon", CLOTHES_TOPS, 75, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camisa larga gris y rayas amarillas", CLOTHES_TOPS, 76, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camisa larga negra", CLOTHES_TOPS, 77, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Sudadera negra capucha", CLOTHES_TOPS, 78, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Jersey negro", CLOTHES_TOPS, 79, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta rojo oscuro y blanco sin letra", CLOTHES_TOPS, 80, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta rojo y blanco P", CLOTHES_TOPS, 81, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camisa negra sencilla", CLOTHES_TOPS, 83, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Polo verde oscuro ANDREAS", CLOTHES_TOPS, 84, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Polo verde oscuro ANDREAS por dentro del pantalon", CLOTHES_TOPS, 85, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camisa vaquera por dentro del pantalon", CLOTHES_TOPS, 86, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Sudadera azul con franja blanca F", CLOTHES_TOPS, 87, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta marron", CLOTHES_TOPS, 88, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta marron por dentro del pantalon", CLOTHES_TOPS, 89, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta blanca abierta", CLOTHES_TOPS, 90, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta blanca cerrada", CLOTHES_TOPS, 91, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta roja abierta", CLOTHES_TOPS, 92, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta roja cerrada", CLOTHES_TOPS, 93, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta oscura con flores marrones abierta", CLOTHES_TOPS, 94, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta oscura con flores marrones cerrada", CLOTHES_TOPS, 95, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camisa blanca con flores", CLOTHES_TOPS, 96, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta larga azul oscuro", CLOTHES_TOPS, 97, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camisa blanca cerrada con bolsillos grandes", CLOTHES_TOPS, 98, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta roja y negra larga", CLOTHES_TOPS, 99, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta blanca sin mangas", CLOTHES_TOPS, 100, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Bikini rosa", CLOTHES_TOPS, 101, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta marron oscuro larga cerrada", CLOTHES_TOPS, 102, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta manga larga gris por dentro del pantalon", CLOTHES_TOPS, 103, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta marron claro cerrada", CLOTHES_TOPS, 104, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camisa blanca ombligo fuera", CLOTHES_TOPS, 105, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Sudadera roja blanca y negra", CLOTHES_TOPS, 106, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Gabardina negra abierta", CLOTHES_TOPS, 107, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Mama noel sucio", CLOTHES_TOPS, 108, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camisa cerrada roja y negra", CLOTHES_TOPS, 109, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta cuero negra y verde", CLOTHES_TOPS, 110, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Palabra de honor marron y blanco", CLOTHES_TOPS, 111, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Vestido azul y negro", CLOTHES_TOPS, 112, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Vestido marron y negro", CLOTHES_TOPS, 113, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Vestido rojo y dorado", CLOTHES_TOPS, 114, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Vestido verde oscuro y amarillo", CLOTHES_TOPS, 115, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Vestido blanco y gris", CLOTHES_TOPS, 116, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta blanca sin manga por dentro del pantalon", CLOTHES_TOPS, 117, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta blanca sin manga", CLOTHES_TOPS, 118, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Polo blanco grande", CLOTHES_TOPS, 119, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camisa de cuadros negro y azulado abierto", CLOTHES_TOPS, 120, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camisa de cuadros negro y azulado cerrado", CLOTHES_TOPS, 121, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta camel cerrada larga", CLOTHES_TOPS, 122, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta rayas negras y blancas", CLOTHES_TOPS, 123, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta larga verde", CLOTHES_TOPS, 125, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta negra larga", CLOTHES_TOPS, 126, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta negra triangulo rojo y blanco", CLOTHES_TOPS, 127, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Polo azul LIBERTY", CLOTHES_TOPS, 128, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Polo azul LIBERTY por dentro de pantalones", CLOTHES_TOPS, 129, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Polo blanco y gris remangado", CLOTHES_TOPS, 130, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Sudadera negra LIBERTY con capucha", CLOTHES_TOPS, 131, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camisa remangada negra flores amarillas", CLOTHES_TOPS, 132, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta larga abierta gris oscuro", CLOTHES_TOPS, 133, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Jersey sin mangas rombos negros gris y blanco", CLOTHES_TOPS, 134, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta marron oscuro con cinturon", CLOTHES_TOPS, 135, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta manga larga gris por dentro de pantalones", CLOTHES_TOPS, 136, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta azul larga escotada", CLOTHES_TOPS, 137, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Sudadera azul con franjas blancas", CLOTHES_TOPS, 138, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta larga abierta negra", CLOTHES_TOPS, 139, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Sudadera verde y blanca", CLOTHES_TOPS, 140, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta celeste", CLOTHES_TOPS, 141, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camisa celeste manga larga", CLOTHES_TOPS, 142, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta celeste cerrada", CLOTHES_TOPS, 143, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Sudadera azul y blanca cerrada", CLOTHES_TOPS, 144, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta verde y blanca", CLOTHES_TOPS, 145, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta veig y EEUU", CLOTHES_TOPS, 146, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta negra y amarilla", CLOTHES_TOPS, 147, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camisa abierta oscura con letras amarillas", CLOTHES_TOPS, 148, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta NAGASAK rosa", CLOTHES_TOPS, 149, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Sudadera negra y roja", CLOTHES_TOPS, 150, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Sudadera marron italia", CLOTHES_TOPS, 151, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta amarilla EEUU", CLOTHES_TOPS, 152, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaquetilla negra abierta sin mangas", CLOTHES_TOPS, 154, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaquetilla sin mangas negra con cremallera", CLOTHES_TOPS, 155, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaquetilla negra sin mangas con botones", CLOTHES_TOPS, 156, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaquetilla negra sin mangas abierta cremallera", CLOTHES_TOPS, 157, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta de cuero negra cerrada", CLOTHES_TOPS, 158, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta de cuero negra sin mangas cerrada", CLOTHES_TOPS, 159, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camisa cuero negro abierto", CLOTHES_TOPS, 160, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camisa larga cuero negro media manga", CLOTHES_TOPS, 161, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta azul oscuro manga serpiente blanco", CLOTHES_TOPS, 162, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta cuero negra abierta", CLOTHES_TOPS, 163, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Abrigo rojo michelin", CLOTHES_TOPS, 164, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta oliva", CLOTHES_TOPS, 165, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camisa vaquera abierta", CLOTHES_TOPS, 166, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camisa vaquera abierta sin mangas", CLOTHES_TOPS, 167, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta corta sin mangas negra", CLOTHES_TOPS, 168, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta corta negra", CLOTHES_TOPS, 169, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Top negro suelto", CLOTHES_TOPS, 170, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camisa vaquera recogida", CLOTHES_TOPS, 171, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Abrigo negro capucha", CLOTHES_TOPS, 172, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaquetilla tirantes pinguino", CLOTHES_TOPS, 173, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta vaquera pegatinas abierta", CLOTHES_TOPS, 174, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta vaquera sin mangas pegatina abierta", CLOTHES_TOPS, 175, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta cuero negra pegatinas cerrada", CLOTHES_TOPS, 176, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta cuero negra pegatinas sin mangas", CLOTHES_TOPS, 177, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camisa sin mangas negra pegatinas", CLOTHES_TOPS, 178, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta sin mangas azul oscuro", CLOTHES_TOPS, 179, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta manga larga negra con rayas amarillas", CLOTHES_TOPS, 180, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camisa rayas blancas y negras", CLOTHES_TOPS, 185, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chubasquero verde largo con capucha", CLOTHES_TOPS, 186, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chubasquero verde largo con capucha abierto", CLOTHES_TOPS, 187, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Gabardina negra larga", CLOTHES_TOPS, 189, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chubasquero camel con capucha", CLOTHES_TOPS, 190, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chubasquero largo con capucha camel", CLOTHES_TOPS, 191, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Jersey gris con rombos negros recogido", CLOTHES_TOPS, 192, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta verde camuflaje y negra abierta", CLOTHES_TOPS, 193, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Gabardina gris larga abierta", CLOTHES_TOPS, 194, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta recogida negra letras rosas", CLOTHES_TOPS, 195, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Jersey navideño", CLOTHES_TOPS, 196, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Jersey remangado gris con bits amarillos", CLOTHES_TOPS, 198, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camisa azul arboles blancos", CLOTHES_TOPS, 200, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Sudadera amarilla parches naranjas", CLOTHES_TOPS, 202, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta ajustada negra con rayas amarillas celestes verdes y rosa", CLOTHES_TOPS, 203, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta sin manga negra con capucha puesta", CLOTHES_TOPS, 204, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Sudadera amarilla parches naranjas con capucha puesta", CLOTHES_TOPS, 205, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Gabardina negra con capucha puesta", CLOTHES_TOPS, 206, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta sin mangas negra con cuello ancho", CLOTHES_TOPS, 207, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta camuflaje sin mangas por dentro del pantalon", CLOTHES_TOPS, 208, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta camuflaje sin mangas", CLOTHES_TOPS, 209, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta camuflaje azulado sin mangas con capucha", CLOTHES_TOPS, 210, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta camuflaje azulado sin mangas capucha puesta", CLOTHES_TOPS, 211, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta camuflaje azulado metida por pantalon", CLOTHES_TOPS, 212, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chubasquero camuflaje azulado con capucha", CLOTHES_TOPS, 214, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chubasquero camuflaje azulado con capucha puesta", CLOTHES_TOPS, 215, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chubasquero camuflaje azulado abierto", CLOTHES_TOPS, 216, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta sin mangas negro y camuflaje azulado", CLOTHES_TOPS, 217, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta manga larga negra con camuflaje azulado", CLOTHES_TOPS, 218, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta camuflaje azulado y negro abierta", CLOTHES_TOPS, 219, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta camuflaje azulado y negro sin mangas abierta", CLOTHES_TOPS, 220, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta camuflaje azulado sin mangas recogido ", CLOTHES_TOPS, 221, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta camuflaje azulado recogido", CLOTHES_TOPS, 222, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Top suelto camuflaje azulado", CLOTHES_TOPS, 223, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta camuflaje azulado por dentro del pantalon", CLOTHES_TOPS, 224, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta sin mangas camuflaje azulado", CLOTHES_TOPS, 225, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta sin mangas camuflaje azulado metido por pantalon", CLOTHES_TOPS, 226, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chubasquero marron largo cerrado", CLOTHES_TOPS, 227, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chubasquero marron largo cerrado capucha puesta", CLOTHES_TOPS, 228, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaquetilla camuflaje azulado sin manga", CLOTHES_TOPS, 229, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Sudadera azul y camuflaje azulado mangas largas", CLOTHES_TOPS, 230, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta mangas largas camuflaje azulado", CLOTHES_TOPS, 231, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camisa camuflaje azulado", CLOTHES_TOPS, 232, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta sin mangas cerrada por cremallera negra", CLOTHES_TOPS, 233, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Chaqueta negra con mangas largas cerrada con cremallera", CLOTHES_TOPS, 234, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta blanca y mangas azules con letras", CLOTHES_TOPS, 235, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "Camiseta negra normal", CLOTHES_TOPS, 236, SEX_FEMALE, 0),
            
            // Male tops
            new BusinessClothesModel(0, "Camiseta roja mangas gris", CLOTHES_TOPS, 0, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta blanca", CLOTHES_TOPS, 1, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta sin mangas cuadros negro y blanco", CLOTHES_TOPS, 2, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camisa abierta blanca", CLOTHES_TOPS, 3, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camisa abierta negra", CLOTHES_TOPS, 4, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta tirantes blanca", CLOTHES_TOPS, 5, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sudadera abierta negra con rayas blancas", CLOTHES_TOPS, 6, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sudadera abierta blanca", CLOTHES_TOPS, 7, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta manga remangada azul y rojo", CLOTHES_TOPS, 8, SEX_MALE, 0),
            new BusinessClothesModel(0, "Polo blanco y negro con rayas negras", CLOTHES_TOPS, 9, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta escotada negra", CLOTHES_TOPS, 10, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaquetilla cerrada gris", CLOTHES_TOPS, 11, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camisa blanca", CLOTHES_TOPS, 12, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camisa blanca por dentro del pantalon", CLOTHES_TOPS, 13, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camisa azul cuadrados azul y blanca", CLOTHES_TOPS, 14, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sin camiseta", CLOTHES_TOPS, 15, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta gris", CLOTHES_TOPS, 16, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta tirantes azul", CLOTHES_TOPS, 17, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta papa noel", CLOTHES_TOPS, 18, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta roja y blanca escote", CLOTHES_TOPS, 19, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta blanca pañuelo rojo", CLOTHES_TOPS, 20, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaquetilla gris claro sin mangas", CLOTHES_TOPS, 21, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta blanca basica", CLOTHES_TOPS, 22, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta oliva abierta", CLOTHES_TOPS, 23, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta gris claro escote", CLOTHES_TOPS, 24, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaquetilla amarilla sin mangas", CLOTHES_TOPS, 25, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camisa negra remangada dentro del pantalon", CLOTHES_TOPS, 26, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta larga negra", CLOTHES_TOPS, 27, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta larga negra escote", CLOTHES_TOPS, 28, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta larga negra abierta", CLOTHES_TOPS, 29, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta rayas blanca y gris", CLOTHES_TOPS, 33, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta negra basica", CLOTHES_TOPS, 34, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta abierta negra rayas blancas", CLOTHES_TOPS, 35, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta tirantas blanco gris negro y azul", CLOTHES_TOPS, 36, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta marron y camel", CLOTHES_TOPS, 37, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta remangada gris y negra", CLOTHES_TOPS, 38, SEX_MALE, 0),
            new BusinessClothesModel(0, "Polo rojo azul y blanco", CLOTHES_TOPS, 39, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaquetilla sin mangas roja", CLOTHES_TOPS, 40, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camisa cuadros camel marron naranja", CLOTHES_TOPS, 41, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camisa remangada azul tirantes", CLOTHES_TOPS, 42, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta verde lima", CLOTHES_TOPS, 44, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta sin manga EEUU", CLOTHES_TOPS, 45, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta abierta azul roja y blanca", CLOTHES_TOPS, 46, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta azul basica", CLOTHES_TOPS, 47, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sudadera bolsillos verde enganches", CLOTHES_TOPS, 48, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sudadera negra", CLOTHES_TOPS, 49, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta manga larga papa noel", CLOTHES_TOPS, 51, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta manga larga roja y blanca", CLOTHES_TOPS, 52, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta manga larga negra por dentro del pantalon", CLOTHES_TOPS, 53, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sudadera bolsillos negra enganches", CLOTHES_TOPS, 54, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta blanca sucia", CLOTHES_TOPS, 56, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sudadera gris con capucha", CLOTHES_TOPS, 57, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta negra abierta", CLOTHES_TOPS, 58, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta gris larga abierta", CLOTHES_TOPS, 59, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta gris larga escote", CLOTHES_TOPS, 60, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sudadera marron cerrada cremallera", CLOTHES_TOPS, 61, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta negra larga abierta", CLOTHES_TOPS, 62, SEX_MALE, 0),
            new BusinessClothesModel(0, "Polo negro basico", CLOTHES_TOPS, 63, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta escote negra", CLOTHES_TOPS, 64, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camisa roja por dentro pantalon", CLOTHES_TOPS, 65, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta cortada por abajo roja y manga negra", CLOTHES_TOPS, 66, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta manga larga blanca", CLOTHES_TOPS, 67, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sudadera negra capucha puesta", CLOTHES_TOPS, 68, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sudadera negra capucha quitada", CLOTHES_TOPS, 69, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta larga marron con pelito por cuello", CLOTHES_TOPS, 70, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta amarilla", CLOTHES_TOPS, 71, SEX_MALE, 0),
            new BusinessClothesModel(0, "Gabardina amarilla", CLOTHES_TOPS, 72, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta motivos amarillos y marron", CLOTHES_TOPS, 73, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sudadera abierta marron motivos amarillos", CLOTHES_TOPS, 74, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sudadera marron motivos amarillos", CLOTHES_TOPS, 75, SEX_MALE, 0),
            new BusinessClothesModel(0, "Gabardina camel cerrada cinturon", CLOTHES_TOPS, 76, SEX_MALE, 0),
            new BusinessClothesModel(0, "Gabardina gris abierta cuello levantado", CLOTHES_TOPS, 77, SEX_MALE, 0),
            new BusinessClothesModel(0, "Jersey cuadros amarillos y marron", CLOTHES_TOPS, 78, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sudadera rojo y blanco W", CLOTHES_TOPS, 79, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta larga blanca", CLOTHES_TOPS, 80, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta larga negra", CLOTHES_TOPS, 81, SEX_MALE, 0),
            new BusinessClothesModel(0, "Polo largo gris", CLOTHES_TOPS, 82, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta larga gris rayas amarillas", CLOTHES_TOPS, 83, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sudadera blanca letras frontal", CLOTHES_TOPS, 84, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sudadera azul oscuro", CLOTHES_TOPS, 85, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sudadera negra con capucha", CLOTHES_TOPS, 86, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sudadera roja y blanca P", CLOTHES_TOPS, 87, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sudadera roja y blanca P abierta", CLOTHES_TOPS, 88, SEX_MALE, 0),
            new BusinessClothesModel(0, "Jersey negro", CLOTHES_TOPS, 89, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sudadera rojo oscuro y blanco", CLOTHES_TOPS, 90, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camisa negra larga", CLOTHES_TOPS, 92, SEX_MALE, 0),
            new BusinessClothesModel(0, "Polo verde oscuro ANDREAS", CLOTHES_TOPS, 93, SEX_MALE, 0),
            new BusinessClothesModel(0, "Polo verde oscuro ANDREAS por dentro del pantalon", CLOTHES_TOPS, 94, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camisa vaquera remangada por dentro del pantalon", CLOTHES_TOPS, 95, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sudadera azul franja diagonal blanca", CLOTHES_TOPS, 96, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta marron", CLOTHES_TOPS, 97, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta manga larga marron por dentro del pantalon", CLOTHES_TOPS, 98, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta blanca abierta", CLOTHES_TOPS, 99, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta blanca escote", CLOTHES_TOPS, 100, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta rojo oscuro abierta", CLOTHES_TOPS, 101, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta rojo oscuro escote", CLOTHES_TOPS, 102, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta marron motivos marron claro abierta", CLOTHES_TOPS, 103, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta marron motivos marron claro escote", CLOTHES_TOPS, 104, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camisa con motivos rojos y azules", CLOTHES_TOPS, 105, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camisa abierta negra", CLOTHES_TOPS, 106, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta blanca bolsillos cerrada", CLOTHES_TOPS, 107, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta rojo oscuro y negro escote", CLOTHES_TOPS, 108, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta sin mangas blanca", CLOTHES_TOPS, 109, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta cuero marron oscuro", CLOTHES_TOPS, 110, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta manga larga gris por dentro del pantalon", CLOTHES_TOPS, 111, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camisa marron claro escote", CLOTHES_TOPS, 112, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sudadera roja blanca y negra", CLOTHES_TOPS, 113, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camisa escote blanca por dentro de pantalon", CLOTHES_TOPS, 114, SEX_MALE, 0),
            new BusinessClothesModel(0, "Gabardina marron oscuro abierta", CLOTHES_TOPS, 115, SEX_MALE, 0),
            new BusinessClothesModel(0, "Papa noel sucio", CLOTHES_TOPS, 116, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camisa cuadros rojo y negro", CLOTHES_TOPS, 117, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta verde y negro abierta", CLOTHES_TOPS, 118, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camisa rayas azul y negro escote", CLOTHES_TOPS, 119, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaquetilla sin manga azul y negro a raya", CLOTHES_TOPS, 120, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta manga larga rayas negra y blanca", CLOTHES_TOPS, 121, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta negra abierta", CLOTHES_TOPS, 122, SEX_MALE, 0),
            new BusinessClothesModel(0, "Polo blanco", CLOTHES_TOPS, 123, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sudadera verde oscuro cerrada", CLOTHES_TOPS, 124, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta camel", CLOTHES_TOPS, 125, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camisa cuadros aguamarina y negro", CLOTHES_TOPS, 126, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camisa cuadros aguamarina y negro abierta", CLOTHES_TOPS, 127, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta verde larga", CLOTHES_TOPS, 128, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta negra triangulo rojo y blanco", CLOTHES_TOPS, 129, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta negra triangulo rojo y blanco abierta", CLOTHES_TOPS, 130, SEX_MALE, 0),
            new BusinessClothesModel(0, "Polo LIBERTY negro", CLOTHES_TOPS, 131, SEX_MALE, 0),
            new BusinessClothesModel(0, "Polo LIBERTY negro por dentro del pantalon", CLOTHES_TOPS, 132, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camisa remangada marron claro por dentro de pantalon", CLOTHES_TOPS, 133, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sudadera LIBERTY negra", CLOTHES_TOPS, 134, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camisa marron oscuro motivos amarillos", CLOTHES_TOPS, 135, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta marron oscuro larga abierta", CLOTHES_TOPS, 136, SEX_MALE, 0),
            new BusinessClothesModel(0, "Gabardina marron oscuro", CLOTHES_TOPS, 138, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta manga larga gris por dentro de pantalon", CLOTHES_TOPS, 139, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta azul oscuro larga escote", CLOTHES_TOPS, 140, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sudadera azul rayas blancas", CLOTHES_TOPS, 141, SEX_MALE, 0),
            new BusinessClothesModel(0, "Gabardina negra larga abierta", CLOTHES_TOPS, 142, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sudadera verde mangas blancas", CLOTHES_TOPS, 143, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camisa blanca y rayas celestes", CLOTHES_TOPS, 144, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta celeste a rayas", CLOTHES_TOPS, 145, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta blanca normal", CLOTHES_TOPS, 146, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sudadera azul oscuro manga blanca", CLOTHES_TOPS, 147, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sudadera verde y blanco", CLOTHES_TOPS, 148, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camisa marron claro EEUU", CLOTHES_TOPS, 149, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sudadera negra filos amarillos", CLOTHES_TOPS, 150, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta negra abierta letras amarilla", CLOTHES_TOPS, 151, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta rosa y negro manga larga", CLOTHES_TOPS, 152, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sudadera marron oscuro y rojo", CLOTHES_TOPS, 153, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sudadera marron Italia", CLOTHES_TOPS, 154, SEX_MALE, 0),
            new BusinessClothesModel(0, "CAmisa amarilla EEUU", CLOTHES_TOPS, 155, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaquetilla negra abierta sin manga", CLOTHES_TOPS, 157, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaquetilla negra cremallera sin manga", CLOTHES_TOPS, 158, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaquetilla negra botones sin manga", CLOTHES_TOPS, 159, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaquetilla negra cremallera abierta", CLOTHES_TOPS, 160, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta negra cuero", CLOTHES_TOPS, 161, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta negra cuero sin manga", CLOTHES_TOPS, 162, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camisa negra larga", CLOTHES_TOPS, 164, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camisa azul oscuro mangas serpiente blanco", CLOTHES_TOPS, 165, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta negra abierta", CLOTHES_TOPS, 166, SEX_MALE, 0),
            new BusinessClothesModel(0, "Abrigo rojo michelin", CLOTHES_TOPS, 167, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sudadera marron oscuro capucha", CLOTHES_TOPS, 168, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camisa vaquera larga abierta", CLOTHES_TOPS, 169, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camisa vaquera abierta larga", CLOTHES_TOPS, 170, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sudadera negra capucha", CLOTHES_TOPS, 171, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta vaquera parches", CLOTHES_TOPS, 172, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta vaquera sin mangas parches", CLOTHES_TOPS, 173, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta cuero parches", CLOTHES_TOPS, 174, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta cuero sin manga parches", CLOTHES_TOPS, 175, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camisa negra letras rojas", CLOTHES_TOPS, 176, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camisa azul oscuro sin manga", CLOTHES_TOPS, 177, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta manga larga negra y amarilla", CLOTHES_TOPS, 178, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta cuero sin manga abierta", CLOTHES_TOPS, 179, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta cuero sin manga", CLOTHES_TOPS, 180, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta cuero abierta", CLOTHES_TOPS, 181, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sudadera negra capucha", CLOTHES_TOPS, 182, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta negra y blanca a rayas", CLOTHES_TOPS, 183, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta verde oscuro larga capucha", CLOTHES_TOPS, 184, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta verde oscuro larga abierta", CLOTHES_TOPS, 185, SEX_MALE, 0),
            new BusinessClothesModel(0, "Gabardina negra", CLOTHES_TOPS, 187, SEX_MALE, 0),
            new BusinessClothesModel(0, "Gabardina camel capucha", CLOTHES_TOPS, 188, SEX_MALE, 0),
            new BusinessClothesModel(0, "Gabardina camel capucha abierta", CLOTHES_TOPS, 189, SEX_MALE, 0),
            new BusinessClothesModel(0, "Jersey gris rombos negros", CLOTHES_TOPS, 190, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta negra y verde camuflaje abierta", CLOTHES_TOPS, 191, SEX_MALE, 0),
            new BusinessClothesModel(0, "Gabardina gris abierta", CLOTHES_TOPS, 192, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta blanca letras naranja", CLOTHES_TOPS, 193, SEX_MALE, 0),
            new BusinessClothesModel(0, "Navideño", CLOTHES_TOPS, 194, SEX_MALE, 0),
            new BusinessClothesModel(0, "Jersey gris con bits amarillo", CLOTHES_TOPS, 196, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camisa azul arboles blancos", CLOTHES_TOPS, 198, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sudadera amarilla parches naranjas capucha", CLOTHES_TOPS, 200, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta negra manga larga dibujos amarillos rojos y celeste", CLOTHES_TOPS, 201, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta negra sin manga con capucha puesta", CLOTHES_TOPS, 202, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sudadera amarilla parche naranja capucha puesta", CLOTHES_TOPS, 203, SEX_MALE, 0),
            new BusinessClothesModel(0, "Gabardina negra larga cacpucha puesta", CLOTHES_TOPS, 204, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sudadera sin manga cuello ancho", CLOTHES_TOPS, 205, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta sin manga camuflaje azul capucha", CLOTHES_TOPS, 206, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta sin manga camuflaje azul capucha puesta", CLOTHES_TOPS, 207, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta camuflaje azul", CLOTHES_TOPS, 208, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta larga azul camuflaje", CLOTHES_TOPS, 209, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta larga azul camuflaje capucha", CLOTHES_TOPS, 210, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta larga azul camuflaje capucha puesta", CLOTHES_TOPS, 211, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta larga abierta azul camuflaje", CLOTHES_TOPS, 212, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta negra y azul camuflaje sin manga", CLOTHES_TOPS, 213, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta negra y azul camuflaje", CLOTHES_TOPS, 214, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta azul camuflaje y negra", CLOTHES_TOPS, 215, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta sin manga azul camuflaje y negra", CLOTHES_TOPS, 216, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta marron larga", CLOTHES_TOPS, 217, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta marron larga capucha puesta", CLOTHES_TOPS, 218, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta sin manga azul camuflaje", CLOTHES_TOPS, 219, SEX_MALE, 0),
            new BusinessClothesModel(0, "Sudadera azul mangas azul camuflaje", CLOTHES_TOPS, 220, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta manga larga azul camuflaje", CLOTHES_TOPS, 221, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta azul camuflaje", CLOTHES_TOPS, 222, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta sin manga negra cremallera", CLOTHES_TOPS, 223, SEX_MALE, 0),
            new BusinessClothesModel(0, "Chaqueta negra cremallera", CLOTHES_TOPS, 224, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta blanca y azul oscuro 98", CLOTHES_TOPS, 225, SEX_MALE, 0),
            new BusinessClothesModel(0, "Camiseta negra normal", CLOTHES_TOPS, 226, SEX_MALE, 0),

            // Female undershirt
            new BusinessClothesModel(0, "0", CLOTHES_UNDERSHIRT, 0, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "1", CLOTHES_UNDERSHIRT, 1, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "2", CLOTHES_UNDERSHIRT, 2, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "3", CLOTHES_UNDERSHIRT, 3, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "4", CLOTHES_UNDERSHIRT, 4, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "5", CLOTHES_UNDERSHIRT, 5, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "6", CLOTHES_UNDERSHIRT, 6, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "7", CLOTHES_UNDERSHIRT, 7, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "8", CLOTHES_UNDERSHIRT, 8, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "9", CLOTHES_UNDERSHIRT, 9, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "10", CLOTHES_UNDERSHIRT, 10, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "11", CLOTHES_UNDERSHIRT, 11, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "12", CLOTHES_UNDERSHIRT, 12, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "13", CLOTHES_UNDERSHIRT, 13, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "14", CLOTHES_UNDERSHIRT, 14, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "15", CLOTHES_UNDERSHIRT, 15, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "16", CLOTHES_UNDERSHIRT, 16, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "17", CLOTHES_UNDERSHIRT, 17, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "18", CLOTHES_UNDERSHIRT, 18, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "19", CLOTHES_UNDERSHIRT, 19, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "20", CLOTHES_UNDERSHIRT, 20, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "21", CLOTHES_UNDERSHIRT, 21, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "22", CLOTHES_UNDERSHIRT, 22, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "23", CLOTHES_UNDERSHIRT, 23, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "24", CLOTHES_UNDERSHIRT, 24, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "25", CLOTHES_UNDERSHIRT, 25, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "26", CLOTHES_UNDERSHIRT, 26, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "27", CLOTHES_UNDERSHIRT, 27, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "28", CLOTHES_UNDERSHIRT, 28, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "29", CLOTHES_UNDERSHIRT, 29, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "30", CLOTHES_UNDERSHIRT, 30, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "31", CLOTHES_UNDERSHIRT, 31, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "32", CLOTHES_UNDERSHIRT, 32, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "33", CLOTHES_UNDERSHIRT, 33, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "34", CLOTHES_UNDERSHIRT, 34, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "35", CLOTHES_UNDERSHIRT, 35, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "36", CLOTHES_UNDERSHIRT, 36, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "37", CLOTHES_UNDERSHIRT, 37, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "38", CLOTHES_UNDERSHIRT, 38, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "39", CLOTHES_UNDERSHIRT, 39, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "40", CLOTHES_UNDERSHIRT, 40, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "41", CLOTHES_UNDERSHIRT, 41, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "42", CLOTHES_UNDERSHIRT, 42, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "43", CLOTHES_UNDERSHIRT, 43, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "44", CLOTHES_UNDERSHIRT, 44, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "45", CLOTHES_UNDERSHIRT, 45, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "46", CLOTHES_UNDERSHIRT, 46, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "47", CLOTHES_UNDERSHIRT, 47, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "48", CLOTHES_UNDERSHIRT, 48, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "49", CLOTHES_UNDERSHIRT, 49, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "50", CLOTHES_UNDERSHIRT, 50, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "51", CLOTHES_UNDERSHIRT, 51, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "52", CLOTHES_UNDERSHIRT, 52, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "53", CLOTHES_UNDERSHIRT, 53, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "54", CLOTHES_UNDERSHIRT, 54, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "55", CLOTHES_UNDERSHIRT, 55, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "56", CLOTHES_UNDERSHIRT, 56, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "57", CLOTHES_UNDERSHIRT, 57, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "58", CLOTHES_UNDERSHIRT, 58, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "59", CLOTHES_UNDERSHIRT, 59, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "60", CLOTHES_UNDERSHIRT, 60, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "61", CLOTHES_UNDERSHIRT, 61, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "62", CLOTHES_UNDERSHIRT, 62, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "63", CLOTHES_UNDERSHIRT, 63, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "64", CLOTHES_UNDERSHIRT, 64, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "65", CLOTHES_UNDERSHIRT, 65, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "66", CLOTHES_UNDERSHIRT, 66, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "67", CLOTHES_UNDERSHIRT, 67, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "68", CLOTHES_UNDERSHIRT, 68, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "69", CLOTHES_UNDERSHIRT, 69, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "70", CLOTHES_UNDERSHIRT, 70, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "71", CLOTHES_UNDERSHIRT, 71, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "72", CLOTHES_UNDERSHIRT, 72, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "73", CLOTHES_UNDERSHIRT, 73, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "74", CLOTHES_UNDERSHIRT, 74, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "75", CLOTHES_UNDERSHIRT, 75, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "76", CLOTHES_UNDERSHIRT, 76, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "77", CLOTHES_UNDERSHIRT, 77, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "78", CLOTHES_UNDERSHIRT, 78, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "79", CLOTHES_UNDERSHIRT, 79, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "80", CLOTHES_UNDERSHIRT, 80, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "81", CLOTHES_UNDERSHIRT, 81, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "82", CLOTHES_UNDERSHIRT, 82, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "83", CLOTHES_UNDERSHIRT, 83, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "84", CLOTHES_UNDERSHIRT, 84, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "85", CLOTHES_UNDERSHIRT, 85, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "86", CLOTHES_UNDERSHIRT, 86, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "87", CLOTHES_UNDERSHIRT, 87, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "88", CLOTHES_UNDERSHIRT, 88, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "89", CLOTHES_UNDERSHIRT, 89, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "90", CLOTHES_UNDERSHIRT, 90, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "91", CLOTHES_UNDERSHIRT, 91, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "92", CLOTHES_UNDERSHIRT, 92, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "93", CLOTHES_UNDERSHIRT, 93, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "94", CLOTHES_UNDERSHIRT, 94, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "95", CLOTHES_UNDERSHIRT, 95, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "96", CLOTHES_UNDERSHIRT, 96, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "97", CLOTHES_UNDERSHIRT, 97, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "98", CLOTHES_UNDERSHIRT, 98, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "99", CLOTHES_UNDERSHIRT, 99, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "100", CLOTHES_UNDERSHIRT, 100, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "101", CLOTHES_UNDERSHIRT, 101, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "102", CLOTHES_UNDERSHIRT, 102, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "103", CLOTHES_UNDERSHIRT, 103, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "104", CLOTHES_UNDERSHIRT, 104, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "105", CLOTHES_UNDERSHIRT, 105, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "106", CLOTHES_UNDERSHIRT, 106, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "107", CLOTHES_UNDERSHIRT, 107, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "108", CLOTHES_UNDERSHIRT, 108, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "109", CLOTHES_UNDERSHIRT, 109, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "110", CLOTHES_UNDERSHIRT, 100, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "111", CLOTHES_UNDERSHIRT, 111, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "112", CLOTHES_UNDERSHIRT, 112, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "113", CLOTHES_UNDERSHIRT, 113, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "114", CLOTHES_UNDERSHIRT, 114, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "115", CLOTHES_UNDERSHIRT, 115, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "116", CLOTHES_UNDERSHIRT, 116, SEX_FEMALE, 0),
            new BusinessClothesModel(0, "117", CLOTHES_UNDERSHIRT, 117, SEX_FEMALE, 0),

            // Male undershirt
            new BusinessClothesModel(0, "0", CLOTHES_UNDERSHIRT, 0, SEX_MALE, 0),
            new BusinessClothesModel(0, "1", CLOTHES_UNDERSHIRT, 1, SEX_MALE, 0),
            new BusinessClothesModel(0, "2", CLOTHES_UNDERSHIRT, 2, SEX_MALE, 0),
            new BusinessClothesModel(0, "3", CLOTHES_UNDERSHIRT, 3, SEX_MALE, 0),
            new BusinessClothesModel(0, "4", CLOTHES_UNDERSHIRT, 4, SEX_MALE, 0),
            new BusinessClothesModel(0, "5", CLOTHES_UNDERSHIRT, 5, SEX_MALE, 0),
            new BusinessClothesModel(0, "6", CLOTHES_UNDERSHIRT, 6, SEX_MALE, 0),
            new BusinessClothesModel(0, "7", CLOTHES_UNDERSHIRT, 7, SEX_MALE, 0),
            new BusinessClothesModel(0, "8", CLOTHES_UNDERSHIRT, 8, SEX_MALE, 0),
            new BusinessClothesModel(0, "9", CLOTHES_UNDERSHIRT, 9, SEX_MALE, 0),
            new BusinessClothesModel(0, "10", CLOTHES_UNDERSHIRT, 10, SEX_MALE, 0),
            new BusinessClothesModel(0, "11", CLOTHES_UNDERSHIRT, 11, SEX_MALE, 0),
            new BusinessClothesModel(0, "12", CLOTHES_UNDERSHIRT, 12, SEX_MALE, 0),
            new BusinessClothesModel(0, "13", CLOTHES_UNDERSHIRT, 13, SEX_MALE, 0),
            new BusinessClothesModel(0, "14", CLOTHES_UNDERSHIRT, 14, SEX_MALE, 0),
            new BusinessClothesModel(0, "15", CLOTHES_UNDERSHIRT, 15, SEX_MALE, 0),
            new BusinessClothesModel(0, "16", CLOTHES_UNDERSHIRT, 16, SEX_MALE, 0),
            new BusinessClothesModel(0, "17", CLOTHES_UNDERSHIRT, 17, SEX_MALE, 0),
            new BusinessClothesModel(0, "18", CLOTHES_UNDERSHIRT, 18, SEX_MALE, 0),
            new BusinessClothesModel(0, "19", CLOTHES_UNDERSHIRT, 19, SEX_MALE, 0),
            new BusinessClothesModel(0, "20", CLOTHES_UNDERSHIRT, 20, SEX_MALE, 0),
            new BusinessClothesModel(0, "21", CLOTHES_UNDERSHIRT, 21, SEX_MALE, 0),
            new BusinessClothesModel(0, "22", CLOTHES_UNDERSHIRT, 22, SEX_MALE, 0),
            new BusinessClothesModel(0, "23", CLOTHES_UNDERSHIRT, 23, SEX_MALE, 0),
            new BusinessClothesModel(0, "24", CLOTHES_UNDERSHIRT, 24, SEX_MALE, 0),
            new BusinessClothesModel(0, "25", CLOTHES_UNDERSHIRT, 25, SEX_MALE, 0),
            new BusinessClothesModel(0, "26", CLOTHES_UNDERSHIRT, 26, SEX_MALE, 0),
            new BusinessClothesModel(0, "27", CLOTHES_UNDERSHIRT, 27, SEX_MALE, 0),
            new BusinessClothesModel(0, "28", CLOTHES_UNDERSHIRT, 28, SEX_MALE, 0),
            new BusinessClothesModel(0, "29", CLOTHES_UNDERSHIRT, 29, SEX_MALE, 0),
            new BusinessClothesModel(0, "30", CLOTHES_UNDERSHIRT, 30, SEX_MALE, 0),
            new BusinessClothesModel(0, "31", CLOTHES_UNDERSHIRT, 31, SEX_MALE, 0),
            new BusinessClothesModel(0, "32", CLOTHES_UNDERSHIRT, 32, SEX_MALE, 0),
            new BusinessClothesModel(0, "33", CLOTHES_UNDERSHIRT, 33, SEX_MALE, 0),
            new BusinessClothesModel(0, "34", CLOTHES_UNDERSHIRT, 34, SEX_MALE, 0),
            new BusinessClothesModel(0, "35", CLOTHES_UNDERSHIRT, 35, SEX_MALE, 0),
            new BusinessClothesModel(0, "36", CLOTHES_UNDERSHIRT, 36, SEX_MALE, 0),
            new BusinessClothesModel(0, "37", CLOTHES_UNDERSHIRT, 37, SEX_MALE, 0),
            new BusinessClothesModel(0, "38", CLOTHES_UNDERSHIRT, 38, SEX_MALE, 0),
            new BusinessClothesModel(0, "39", CLOTHES_UNDERSHIRT, 39, SEX_MALE, 0),
            new BusinessClothesModel(0, "40", CLOTHES_UNDERSHIRT, 40, SEX_MALE, 0),
            new BusinessClothesModel(0, "41", CLOTHES_UNDERSHIRT, 41, SEX_MALE, 0),
            new BusinessClothesModel(0, "42", CLOTHES_UNDERSHIRT, 42, SEX_MALE, 0),
            new BusinessClothesModel(0, "43", CLOTHES_UNDERSHIRT, 43, SEX_MALE, 0),
            new BusinessClothesModel(0, "44", CLOTHES_UNDERSHIRT, 44, SEX_MALE, 0),
            new BusinessClothesModel(0, "45", CLOTHES_UNDERSHIRT, 45, SEX_MALE, 0),
            new BusinessClothesModel(0, "46", CLOTHES_UNDERSHIRT, 46, SEX_MALE, 0),
            new BusinessClothesModel(0, "47", CLOTHES_UNDERSHIRT, 47, SEX_MALE, 0),
            new BusinessClothesModel(0, "48", CLOTHES_UNDERSHIRT, 48, SEX_MALE, 0),
            new BusinessClothesModel(0, "49", CLOTHES_UNDERSHIRT, 49, SEX_MALE, 0),
            new BusinessClothesModel(0, "50", CLOTHES_UNDERSHIRT, 50, SEX_MALE, 0),
            new BusinessClothesModel(0, "51", CLOTHES_UNDERSHIRT, 51, SEX_MALE, 0),
            new BusinessClothesModel(0, "52", CLOTHES_UNDERSHIRT, 52, SEX_MALE, 0),
            new BusinessClothesModel(0, "53", CLOTHES_UNDERSHIRT, 53, SEX_MALE, 0),
            new BusinessClothesModel(0, "54", CLOTHES_UNDERSHIRT, 54, SEX_MALE, 0),
            new BusinessClothesModel(0, "55", CLOTHES_UNDERSHIRT, 55, SEX_MALE, 0),
            new BusinessClothesModel(0, "56", CLOTHES_UNDERSHIRT, 56, SEX_MALE, 0),
            new BusinessClothesModel(0, "57", CLOTHES_UNDERSHIRT, 57, SEX_MALE, 0),
            new BusinessClothesModel(0, "58", CLOTHES_UNDERSHIRT, 58, SEX_MALE, 0),
            new BusinessClothesModel(0, "59", CLOTHES_UNDERSHIRT, 59, SEX_MALE, 0),
            new BusinessClothesModel(0, "60", CLOTHES_UNDERSHIRT, 60, SEX_MALE, 0),
            new BusinessClothesModel(0, "61", CLOTHES_UNDERSHIRT, 61, SEX_MALE, 0),
            new BusinessClothesModel(0, "62", CLOTHES_UNDERSHIRT, 62, SEX_MALE, 0),
            new BusinessClothesModel(0, "63", CLOTHES_UNDERSHIRT, 63, SEX_MALE, 0),
            new BusinessClothesModel(0, "64", CLOTHES_UNDERSHIRT, 64, SEX_MALE, 0),
            new BusinessClothesModel(0, "65", CLOTHES_UNDERSHIRT, 65, SEX_MALE, 0),
            new BusinessClothesModel(0, "66", CLOTHES_UNDERSHIRT, 66, SEX_MALE, 0),
            new BusinessClothesModel(0, "67", CLOTHES_UNDERSHIRT, 67, SEX_MALE, 0),
            new BusinessClothesModel(0, "68", CLOTHES_UNDERSHIRT, 68, SEX_MALE, 0),
            new BusinessClothesModel(0, "69", CLOTHES_UNDERSHIRT, 69, SEX_MALE, 0),
            new BusinessClothesModel(0, "70", CLOTHES_UNDERSHIRT, 70, SEX_MALE, 0),
            new BusinessClothesModel(0, "71", CLOTHES_UNDERSHIRT, 71, SEX_MALE, 0),
            new BusinessClothesModel(0, "72", CLOTHES_UNDERSHIRT, 72, SEX_MALE, 0),
            new BusinessClothesModel(0, "73", CLOTHES_UNDERSHIRT, 73, SEX_MALE, 0),
            new BusinessClothesModel(0, "74", CLOTHES_UNDERSHIRT, 74, SEX_MALE, 0),
            new BusinessClothesModel(0, "75", CLOTHES_UNDERSHIRT, 75, SEX_MALE, 0),
            new BusinessClothesModel(0, "76", CLOTHES_UNDERSHIRT, 76, SEX_MALE, 0),
            new BusinessClothesModel(0, "77", CLOTHES_UNDERSHIRT, 77, SEX_MALE, 0),
            new BusinessClothesModel(0, "78", CLOTHES_UNDERSHIRT, 78, SEX_MALE, 0),
            new BusinessClothesModel(0, "79", CLOTHES_UNDERSHIRT, 79, SEX_MALE, 0),
            new BusinessClothesModel(0, "80", CLOTHES_UNDERSHIRT, 80, SEX_MALE, 0),
            new BusinessClothesModel(0, "81", CLOTHES_UNDERSHIRT, 81, SEX_MALE, 0),
            new BusinessClothesModel(0, "82", CLOTHES_UNDERSHIRT, 82, SEX_MALE, 0),
            new BusinessClothesModel(0, "83", CLOTHES_UNDERSHIRT, 83, SEX_MALE, 0),
            new BusinessClothesModel(0, "84", CLOTHES_UNDERSHIRT, 84, SEX_MALE, 0),
            new BusinessClothesModel(0, "85", CLOTHES_UNDERSHIRT, 85, SEX_MALE, 0),
            new BusinessClothesModel(0, "86", CLOTHES_UNDERSHIRT, 86, SEX_MALE, 0),
            new BusinessClothesModel(0, "87", CLOTHES_UNDERSHIRT, 87, SEX_MALE, 0),
            new BusinessClothesModel(0, "88", CLOTHES_UNDERSHIRT, 88, SEX_MALE, 0),
            new BusinessClothesModel(0, "89", CLOTHES_UNDERSHIRT, 89, SEX_MALE, 0),
            new BusinessClothesModel(0, "90", CLOTHES_UNDERSHIRT, 90, SEX_MALE, 0),
            new BusinessClothesModel(0, "91", CLOTHES_UNDERSHIRT, 91, SEX_MALE, 0),
            new BusinessClothesModel(0, "92", CLOTHES_UNDERSHIRT, 92, SEX_MALE, 0),
            new BusinessClothesModel(0, "93", CLOTHES_UNDERSHIRT, 93, SEX_MALE, 0),
            new BusinessClothesModel(0, "94", CLOTHES_UNDERSHIRT, 94, SEX_MALE, 0),
            new BusinessClothesModel(0, "95", CLOTHES_UNDERSHIRT, 95, SEX_MALE, 0),
            new BusinessClothesModel(0, "96", CLOTHES_UNDERSHIRT, 96, SEX_MALE, 0),
            new BusinessClothesModel(0, "97", CLOTHES_UNDERSHIRT, 97, SEX_MALE, 0),

            // Female hats
            new BusinessClothesModel(1, "Auriculares rojos", ACCESSORY_HATS, 0, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Cono blanco", ACCESSORY_HATS, 1, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Cowgirl cuadros negro y blanco", ACCESSORY_HATS, 2, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Achatado cuadros negro y blanco", ACCESSORY_HATS, 3, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Gorra negra Los Santos", ACCESSORY_HATS, 4, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Lana negro", ACCESSORY_HATS, 5, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Gorra negra", ACCESSORY_HATS, 6, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Achatado azul oscuro", ACCESSORY_HATS, 7, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Cuadros negros y blanco", ACCESSORY_HATS, 8, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Gorra negra y blanca Fruit", ACCESSORY_HATS, 9, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Gorra cuadros negro y blanco", ACCESSORY_HATS, 10, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Pamela cuadros negro y blanco", ACCESSORY_HATS, 11, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Lana negro", ACCESSORY_HATS, 12, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Marron claro y marron oscuro", ACCESSORY_HATS, 13, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Pintor negro", ACCESSORY_HATS, 14, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Auriculares blanco", ACCESSORY_HATS, 15, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Casco amarillo rojo y negro", ACCESSORY_HATS, 16, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Casco abierto azul y negro", ACCESSORY_HATS, 17, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Casco negro", ACCESSORY_HATS, 18, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Camel", ACCESSORY_HATS, 20, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Achatado rosa con cinta", ACCESSORY_HATS, 21, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Pamela marron claro", ACCESSORY_HATS, 22, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Papa noel", ACCESSORY_HATS, 23, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Duende", ACCESSORY_HATS, 24, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Reno", ACCESSORY_HATS, 25, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Negro chaplin", ACCESSORY_HATS, 26, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Negro copa", ACCESSORY_HATS, 27, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Marron", ACCESSORY_HATS, 28, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Lila", ACCESSORY_HATS, 29, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Choose you primero", ACCESSORY_HATS, 30, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Choose you segundo", ACCESSORY_HATS, 31, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Choose you tercero", ACCESSORY_HATS, 32, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Capitan america", ACCESSORY_HATS, 33, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Reina patriota", ACCESSORY_HATS, 34, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Estrellas EEUU", ACCESSORY_HATS, 35, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Corra con cerveza", ACCESSORY_HATS, 36, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Casco caballo", ACCESSORY_HATS, 38, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Arbol navidad", ACCESSORY_HATS, 39, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Postre", ACCESSORY_HATS, 40, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Navideño", ACCESSORY_HATS, 41, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Mama noel", ACCESSORY_HATS, 42, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Gorra Naughty", ACCESSORY_HATS, 43, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Gorra hacia atras roja", ACCESSORY_HATS, 44, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Casco visera negro", ACCESSORY_HATS, 47, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Casco negro opaco", ACCESSORY_HATS, 49, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Casco negro espejo", ACCESSORY_HATS, 50, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Gorra verde simbolo", ACCESSORY_HATS, 53, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Pamela marron clarito", ACCESSORY_HATS, 54, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Gorra roja y azul", ACCESSORY_HATS, 55, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Gorra negra Magretics", ACCESSORY_HATS, 56, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Gorra marron clarito", ACCESSORY_HATS, 58, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Casco caballo verde", ACCESSORY_HATS, 59, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Lana verde", ACCESSORY_HATS, 60, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Negro y verde", ACCESSORY_HATS, 61, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Casco verde", ACCESSORY_HATS, 62, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Gorra verde", ACCESSORY_HATS, 63, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Gorra negra triangulo rojo y blanco", ACCESSORY_HATS, 64, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Gorra negra hacia atras", ACCESSORY_HATS, 65, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Casco oscuro abierta visera", ACCESSORY_HATS, 66, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Casco negro abierta visera", ACCESSORY_HATS, 67, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Casco negro visera espejo abierta", ACCESSORY_HATS, 68, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Casco verde visera abierta", ACCESSORY_HATS, 71, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Casco verde abierto", ACCESSORY_HATS, 74, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Gorra amarilla y azul", ACCESSORY_HATS, 75, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Gorra hacia atras azul", ACCESSORY_HATS, 76, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Achatado negro", ACCESSORY_HATS, 82, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Casco guerra con pinchos", ACCESSORY_HATS, 83, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Casco guerra negro", ACCESSORY_HATS, 84, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Casco cresta", ACCESSORY_HATS, 86, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Casco trinchera", ACCESSORY_HATS, 88, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Casco trinchera plata", ACCESSORY_HATS, 89, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Casco negro rayas amarillas", ACCESSORY_HATS, 90, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Casco negro rayas amarillas visera abierta", ACCESSORY_HATS, 91, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Casco abierto blanco y azul", ACCESSORY_HATS, 92, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Achatado marron claro", ACCESSORY_HATS, 93, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Normal negro y camel", ACCESSORY_HATS, 94, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Gorra negra Bigness", ACCESSORY_HATS, 95, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Cuernos reno punta roja", ACCESSORY_HATS, 100, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Gorra negra", ACCESSORY_HATS, 101, SEX_FEMALE, 0),

            // Male hats
            new BusinessClothesModel(1, "Auriculares rojos", ACCESSORY_HATS, 0, SEX_MALE, 0),
            new BusinessClothesModel(1, "Cono blanco", ACCESSORY_HATS, 1, SEX_MALE, 0),
            new BusinessClothesModel(1, "Lana negro", ACCESSORY_HATS, 2, SEX_MALE, 0),
            new BusinessClothesModel(1, "Michael cuadros negros y blanco", ACCESSORY_HATS, 3, SEX_MALE, 0),
            new BusinessClothesModel(1, "Gorra negra LS", ACCESSORY_HATS, 4, SEX_MALE, 0),
            new BusinessClothesModel(1, "Lana negro achatado", ACCESSORY_HATS, 5, SEX_MALE, 0),
            new BusinessClothesModel(1, "Gorra verde", ACCESSORY_HATS, 6, SEX_MALE, 0),
            new BusinessClothesModel(1, "Gorrilla blanca", ACCESSORY_HATS, 7, SEX_MALE, 0),
            new BusinessClothesModel(1, "Gorra hacia atras cuadrados negros y blancos", ACCESSORY_HATS, 9, SEX_MALE, 0),
            new BusinessClothesModel(1, "Gorra cuadrados negros y blancos", ACCESSORY_HATS, 10, SEX_MALE, 0),
            new BusinessClothesModel(1, "Grande negro con cinta", ACCESSORY_HATS, 12, SEX_MALE, 0),
            new BusinessClothesModel(1, "Cowboy negro", ACCESSORY_HATS, 13, SEX_MALE, 0),
            new BusinessClothesModel(1, "Pañuelo blanco motivos negros", ACCESSORY_HATS, 14, SEX_MALE, 0),
            new BusinessClothesModel(1, "Auriculares blancos", ACCESSORY_HATS, 15, SEX_MALE, 0),
            new BusinessClothesModel(1, "Casco amarillo negro y rojo", ACCESSORY_HATS, 16, SEX_MALE, 0),
            new BusinessClothesModel(1, "Casco abierto azul y negro", ACCESSORY_HATS, 17, SEX_MALE, 0),
            new BusinessClothesModel(1, "Casco negro", ACCESSORY_HATS, 18, SEX_MALE, 0),
            new BusinessClothesModel(1, "Pesquero verde", ACCESSORY_HATS, 20, SEX_MALE, 0),
            new BusinessClothesModel(1, "Sombrerillo canera", ACCESSORY_HATS, 21, SEX_MALE, 0),
            new BusinessClothesModel(1, "Papa noel", ACCESSORY_HATS, 22, SEX_MALE, 0),
            new BusinessClothesModel(1, "Duende navidad", ACCESSORY_HATS, 23, SEX_MALE, 0),
            new BusinessClothesModel(1, "Reno", ACCESSORY_HATS, 24, SEX_MALE, 0),
            new BusinessClothesModel(1, "Negro oscuro con cinta", ACCESSORY_HATS, 25, SEX_MALE, 0),
            new BusinessClothesModel(1, "Chaplin negro", ACCESSORY_HATS, 26, SEX_MALE, 0),
            new BusinessClothesModel(1, "Copa negro", ACCESSORY_HATS, 27, SEX_MALE, 0),
            new BusinessClothesModel(1, "Lana azul", ACCESSORY_HATS, 28, SEX_MALE, 0),
            new BusinessClothesModel(1, "Pequeño gris", ACCESSORY_HATS, 29, SEX_MALE, 0),
            new BusinessClothesModel(1, "Grande rojo cinta negra", ACCESSORY_HATS, 30, SEX_MALE, 0),
            new BusinessClothesModel(1, "Choose you primero", ACCESSORY_HATS, 31, SEX_MALE, 0),
            new BusinessClothesModel(1, "Choose you segundo", ACCESSORY_HATS, 32, SEX_MALE, 0),
            new BusinessClothesModel(1, "Choose you tercero", ACCESSORY_HATS, 33, SEX_MALE, 0),
            new BusinessClothesModel(1, "Lana capitan america", ACCESSORY_HATS, 34, SEX_MALE, 0),
            new BusinessClothesModel(1, "Reina america", ACCESSORY_HATS, 35, SEX_MALE, 0),
            new BusinessClothesModel(1, "Estrellas EEUU", ACCESSORY_HATS, 36, SEX_MALE, 0),
            new BusinessClothesModel(1, "Gorra con cerveza", ACCESSORY_HATS, 37, SEX_MALE, 0),
            new BusinessClothesModel(1, "Negro caballo", ACCESSORY_HATS, 39, SEX_MALE, 0),
            new BusinessClothesModel(1, "Arbol navidad", ACCESSORY_HATS, 40, SEX_MALE, 0),
            new BusinessClothesModel(1, "Postre", ACCESSORY_HATS, 41, SEX_MALE, 0),
            new BusinessClothesModel(1, "Navideño", ACCESSORY_HATS, 42, SEX_MALE, 0),
            new BusinessClothesModel(1, "Papa noel a cuadros", ACCESSORY_HATS, 43, SEX_MALE, 0),
            new BusinessClothesModel(1, "Gorra roja y blanca con letras", ACCESSORY_HATS, 44, SEX_MALE, 0),
            new BusinessClothesModel(1, "Gorra hacia atras roja", ACCESSORY_HATS, 45, SEX_MALE, 0),
            new BusinessClothesModel(1, "Casco visera hacia delante negro", ACCESSORY_HATS, 48, SEX_MALE, 0),
            new BusinessClothesModel(1, "Casco negro opaco", ACCESSORY_HATS, 50, SEX_MALE, 0),
            new BusinessClothesModel(1, "Casco negro visera espejo", ACCESSORY_HATS, 51, SEX_MALE, 0),
            new BusinessClothesModel(1, "Gorra verde letra", ACCESSORY_HATS, 54, SEX_MALE, 0),
            new BusinessClothesModel(1, "Gorra roja y azul con letras", ACCESSORY_HATS, 55, SEX_MALE, 0),
            new BusinessClothesModel(1, "Gorra negra con letras doradas", ACCESSORY_HATS, 56, SEX_MALE, 0),
            new BusinessClothesModel(1, "Gorra maron", ACCESSORY_HATS, 58, SEX_MALE, 0),
            new BusinessClothesModel(1, "Casco caballo verde", ACCESSORY_HATS, 59, SEX_MALE, 0),
            new BusinessClothesModel(1, "Gorra verde", ACCESSORY_HATS, 60, SEX_MALE, 0),
            new BusinessClothesModel(1, "Grance negro cinta verde", ACCESSORY_HATS, 61, SEX_MALE, 0),
            new BusinessClothesModel(1, "Casco verde", ACCESSORY_HATS, 62, SEX_MALE, 0),
            new BusinessClothesModel(1, "Gorra verde lisa", ACCESSORY_HATS, 63, SEX_MALE, 0),
            new BusinessClothesModel(1, "Grande azul oscuro con rayas", ACCESSORY_HATS, 64, SEX_MALE, 0),
            new BusinessClothesModel(1, "Gorra negra triangulo rojo y blanco", ACCESSORY_HATS, 65, SEX_MALE, 0),
            new BusinessClothesModel(1, "Gorra negra hacia atras", ACCESSORY_HATS, 66, SEX_MALE, 0),
            new BusinessClothesModel(1, "Casco negro visera abierta", ACCESSORY_HATS, 67, SEX_MALE, 0),
            new BusinessClothesModel(1, "Casco negro visera opaca abierta", ACCESSORY_HATS, 68, SEX_MALE, 0),
            new BusinessClothesModel(1, "Casco negro visera espejo abierta", ACCESSORY_HATS, 69, SEX_MALE, 0),
            new BusinessClothesModel(1, "Casco abierto verde", ACCESSORY_HATS, 75, SEX_MALE, 0),
            new BusinessClothesModel(1, "Gorra azul y amarilla con letras", ACCESSORY_HATS, 76, SEX_MALE, 0),
            new BusinessClothesModel(1, "Gorra azul hacia atras", ACCESSORY_HATS, 77, SEX_MALE, 0),
            new BusinessClothesModel(1, "Lana negra", ACCESSORY_HATS, 83, SEX_MALE, 0),
            new BusinessClothesModel(1, "Casco guerra pinchos", ACCESSORY_HATS, 84, SEX_MALE, 0),
            new BusinessClothesModel(1, "Casco guerra", ACCESSORY_HATS, 85, SEX_MALE, 0),
            new BusinessClothesModel(1, "Casco guerra visera", ACCESSORY_HATS, 86, SEX_MALE, 0),
            new BusinessClothesModel(1, "Casco guerra cresta", ACCESSORY_HATS, 87, SEX_MALE, 0),
            new BusinessClothesModel(1, "Casco guerra negro", ACCESSORY_HATS, 89, SEX_MALE, 0),
            new BusinessClothesModel(1, "Casco guerra plata", ACCESSORY_HATS, 90, SEX_MALE, 0),
            new BusinessClothesModel(1, "Casco negro rayas amarillas", ACCESSORY_HATS, 91, SEX_MALE, 0),
            new BusinessClothesModel(1, "Casco negro rayas amarillas visera abierta", ACCESSORY_HATS, 92, SEX_MALE, 0),
            new BusinessClothesModel(1, "Casco abierto blanco con diana", ACCESSORY_HATS, 93, SEX_MALE, 0),
            new BusinessClothesModel(1, "Pescador camel", ACCESSORY_HATS, 94, SEX_MALE, 0),
            new BusinessClothesModel(1, "Michael negro cinta camel", ACCESSORY_HATS, 95, SEX_MALE, 0),
            new BusinessClothesModel(1, "Gorra negra BIGNESS", ACCESSORY_HATS, 96, SEX_MALE, 0),
            new BusinessClothesModel(1, "Reno puntas rojas", ACCESSORY_HATS, 101, SEX_MALE, 0),
            new BusinessClothesModel(1, "Gorra negra basica", ACCESSORY_HATS, 102, SEX_MALE, 0),

            // Female glasses
            new BusinessClothesModel(1, "Deportiva cristal amarillo naranja", ACCESSORY_GLASSES, 0, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Redondas marron oscuro", ACCESSORY_GLASSES, 1, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Redonda con picos superiores marron oscuro", ACCESSORY_GLASSES, 2, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Rectas cristal marron", ACCESSORY_GLASSES, 3, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Redondas pico superior leopardo", ACCESSORY_GLASSES, 4, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Redonda negra patillas plata", ACCESSORY_GLASSES, 6, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Ovaladas cristal opaco marron", ACCESSORY_GLASSES, 7, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Pasta cristal marron transparente", ACCESSORY_GLASSES, 8, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Deportiva cristal amarillo", ACCESSORY_GLASSES, 9, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Deportiva cristal de amarillo a azul", ACCESSORY_GLASSES, 10, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Aviador plata cristal oscuro", ACCESSORY_GLASSES, 11, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Vista pasta negra cristal transparente", ACCESSORY_GLASSES, 14, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Redonda marron oscuro patillas oro", ACCESSORY_GLASSES, 16, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Pasta negra cristal transparente", ACCESSORY_GLASSES, 17, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Finas oro cristal negro", ACCESSORY_GLASSES, 18, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Finas negro cristal negro", ACCESSORY_GLASSES, 19, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Redondas negras cristal transparente oscuro", ACCESSORY_GLASSES, 20, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Redonda negra cristal transparente", ACCESSORY_GLASSES, 21, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Estrellas EEUU", ACCESSORY_GLASSES, 22, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Cuadradas estrellas azul y blanco", ACCESSORY_GLASSES, 23, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Cuadradas negra cristal oscuro", ACCESSORY_GLASSES, 24, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Deportiva verde cristal oscuro", ACCESSORY_GLASSES, 25, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Piloto", ACCESSORY_GLASSES, 26, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Snow", ACCESSORY_GLASSES, 27, SEX_FEMALE, 0),

            // Male glasses
            new BusinessClothesModel(1, "Finas cuadrados blancos y negros", ACCESSORY_GLASSES, 1, SEX_MALE, 0),
            new BusinessClothesModel(1, "Finas negra cristal oscuro", ACCESSORY_GLASSES, 2, SEX_MALE, 0),
            new BusinessClothesModel(1, "Finas negras cristal transparente oscuro", ACCESSORY_GLASSES, 3, SEX_MALE, 0),
            new BusinessClothesModel(1, "Finas negrasl cristal transparente", ACCESSORY_GLASSES, 4, SEX_MALE, 0),
            new BusinessClothesModel(1, "Aviador dorado", ACCESSORY_GLASSES, 5, SEX_MALE, 0),
            new BusinessClothesModel(1, "Aviador plata", ACCESSORY_GLASSES, 8, SEX_MALE, 0),
            new BusinessClothesModel(1, "Pasta negra cristal negro", ACCESSORY_GLASSES, 9, SEX_MALE, 0),
            new BusinessClothesModel(1, "Cuadrada negra superior oro", ACCESSORY_GLASSES, 10, SEX_MALE, 0),
            new BusinessClothesModel(1, "Aviador oro cristal marron", ACCESSORY_GLASSES, 12, SEX_MALE, 0),
            new BusinessClothesModel(1, "Pasta negra cristal rojo", ACCESSORY_GLASSES, 13, SEX_MALE, 0),
            new BusinessClothesModel(1, "Deportiva cristal amarillo", ACCESSORY_GLASSES, 15, SEX_MALE, 0),
            new BusinessClothesModel(1, "Deportiva negra cristal rojo", ACCESSORY_GLASSES, 16, SEX_MALE, 0),
            new BusinessClothesModel(1, "Fina blanca cristal gris", ACCESSORY_GLASSES, 17, SEX_MALE, 0),
            new BusinessClothesModel(1, "Cuadrada oro cristal oscuro", ACCESSORY_GLASSES, 18, SEX_MALE, 0),
            new BusinessClothesModel(1, "Cuadrada pasta negra superior oro", ACCESSORY_GLASSES, 19, SEX_MALE, 0),
            new BusinessClothesModel(1, "Pasta negra cristal transparente", ACCESSORY_GLASSES, 20, SEX_MALE, 0),
            new BusinessClothesModel(1, "Estrellas EEUU", ACCESSORY_GLASSES, 21, SEX_MALE, 0),
            new BusinessClothesModel(1, "Pasta azul y estrellas blancas", ACCESSORY_GLASSES, 22, SEX_MALE, 0),
            new BusinessClothesModel(1, "Deportiva verde cristal negro", ACCESSORY_GLASSES, 23, SEX_MALE, 0),
            new BusinessClothesModel(1, "Piloto", ACCESSORY_GLASSES, 24, SEX_MALE, 0),
            new BusinessClothesModel(1, "Snow", ACCESSORY_GLASSES, 25, SEX_MALE, 0),

            // Female earrings
            new BusinessClothesModel(1, "Pinganillo negro y blanco", ACCESSORY_EARS, 0, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Pinganillo rojo y negro", ACCESSORY_EARS, 1, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Pinganillo rectangular negro", ACCESSORY_EARS, 2, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Pendiente largo plata", ACCESSORY_EARS, 3, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Pendiente marron caro", ACCESSORY_EARS, 4, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Pendiente plata", ACCESSORY_EARS, 5, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Largo rombo oro", ACCESSORY_EARS, 6, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Largo oro", ACCESSORY_EARS, 7, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Largo bolitas oro", ACCESSORY_EARS, 8, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Cortina oro", ACCESSORY_EARS, 9, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Largo bolita verde", ACCESSORY_EARS, 10, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Largo oro", ACCESSORY_EARS, 11, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Pequeñito", ACCESSORY_EARS, 12, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Aro arma oro", ACCESSORY_EARS, 13, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Aro ancho oro", ACCESSORY_EARS, 14, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Aro fino oro", ACCESSORY_EARS, 15, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Aro ancho oro letras", ACCESSORY_EARS, 16, SEX_FEMALE, 0),
            new BusinessClothesModel(1, "Aro ancho oro letras", ACCESSORY_EARS, 17, SEX_FEMALE, 0),

            // Male earrings
            new BusinessClothesModel(1, "Pinganillo negro y blanco", ACCESSORY_EARS, 0, SEX_MALE, 0),
            new BusinessClothesModel(1, "Pinganillo rojo y negro", ACCESSORY_EARS, 1, SEX_MALE, 0),
            new BusinessClothesModel(1, "Aro pequeño oro", ACCESSORY_EARS, 4, SEX_MALE, 0),
            new BusinessClothesModel(1, "Circulo oro pequeño", ACCESSORY_EARS, 7, SEX_MALE, 0),
            new BusinessClothesModel(1, "Piramide oro", ACCESSORY_EARS, 10, SEX_MALE, 0),
            new BusinessClothesModel(1, "Cuadrado oro", ACCESSORY_EARS, 13, SEX_MALE, 0),
            new BusinessClothesModel(1, "Diamante", ACCESSORY_EARS, 16, SEX_MALE, 0),
            new BusinessClothesModel(1, "Espino", ACCESSORY_EARS, 22, SEX_MALE, 0),
            new BusinessClothesModel(1, "Calavera plata", ACCESSORY_EARS, 25, SEX_MALE, 0),
            new BusinessClothesModel(1, "Pincho metal", ACCESSORY_EARS, 28, SEX_MALE, 0),
            new BusinessClothesModel(1, "Cuadrado pequeño negro", ACCESSORY_EARS, 31, SEX_MALE, 0),
            new BusinessClothesModel(1, "Cuadrado grande plata", ACCESSORY_EARS, 35, SEX_MALE, 0)
        };

        // Tattoo list
        public static List<BusinessTattooModel> TATTOO_LIST = new List<BusinessTattooModel>
        {
            // Torso
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Refined Hustler", "mpbusiness_overlays", "MP_Buis_M_Stomach_000", string.Empty, 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Rich", "mpbusiness_overlays", "MP_Buis_M_Chest_000", string.Empty, 150),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "$$$", "mpbusiness_overlays", "MP_Buis_M_Chest_001", string.Empty, 150),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Makin' Paper", "mpbusiness_overlays", "MP_Buis_M_Back_000", string.Empty, 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "High Roller", "mpbusiness_overlays", string.Empty, "MP_Buis_F_Chest_000", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Makin' Money", "mpbusiness_overlays", string.Empty, "MP_Buis_F_Chest_001", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Love Money", "mpbusiness_overlays", string.Empty, "MP_Buis_F_Chest_002", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Diamond Back", "mpbusiness_overlays", string.Empty, "MP_Buis_F_Stom_000", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Santo Capra Logo", "mpbusiness_overlays", string.Empty, "MP_Buis_F_Stom_001", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Money Bag", "mpbusiness_overlays", string.Empty, "MP_Buis_F_Stom_002", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Respect", "mpbusiness_overlays", string.Empty, "MP_Buis_F_Back_000", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Gold Digger", "mpbusiness_overlays", string.Empty, "MP_Buis_F_Back_001", 150),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Carp Outline", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_005", "MP_Xmas2_F_Tat_005", 230),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Carp Shaded", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_006", "MP_Xmas2_F_Tat_006", 350),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Time To Die", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_009", "MP_Xmas2_F_Tat_009", 250),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Roaring Tiger", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_011", "MP_Xmas2_F_Tat_011", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Lizard", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_013", "MP_Xmas2_F_Tat_013", 250),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Japanese Warrior", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_015", "MP_Xmas2_F_Tat_015", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Loose Lips Outline", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_016", "MP_Xmas2_F_Tat_016", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Loose Lips Color", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_017", "MP_Xmas2_F_Tat_017", 250),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Royal Dagger Outline", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_018", "MP_Xmas2_F_Tat_018", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Royal Dagger Color", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_019", "MP_Xmas2_F_Tat_019", 350),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Executioner", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_028", "MP_Xmas2_F_Tat_028", 250),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Bullet Proof", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_000_M", "MP_Gunrunning_Tattoo_000_F", 320),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Crossed Weapons", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_001_M", "MP_Gunrunning_Tattoo_001_F", 320),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Butterfly Knife", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_009_M", "MP_Gunrunning_Tattoo_009_F", 320),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Cash Money", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_010_M", "MP_Gunrunning_Tattoo_010_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Dollar Daggers", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_012_M", "MP_Gunrunning_Tattoo_012_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Wolf Insignia", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_013_M", "MP_Gunrunning_Tattoo_013_F", 450),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Backstabber", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_014_M", "MP_Gunrunning_Tattoo_014_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Dog Tags", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_017_M", "MP_Gunrunning_Tattoo_017_F", 120),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Dual Wield Skull", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_018_M", "MP_Gunrunning_Tattoo_018_F", 270),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Pistol Wings", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_019_M", "MP_Gunrunning_Tattoo_019_F", 350),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Crowned Weapons", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_020_M", "MP_Gunrunning_Tattoo_020_F", 350),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Explosive Heart", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_022_M", "MP_Gunrunning_Tattoo_022_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Micro SMG Chain", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_028_M", "MP_Gunrunning_Tattoo_028_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Win Some Lose Some", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_029_M", "MP_Gunrunning_Tattoo_029_F", 280),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Crossed Arrows", "mphipster_overlays", "FM_Hip_M_Tat_000", "FM_Hip_F_Tat_000", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Chemistry", "mphipster_overlays", "FM_Hip_M_Tat_002", "FM_Hip_F_Tat_002", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Feather Birds", "mphipster_overlays", "FM_Hip_M_Tat_006", "FM_Hip_F_Tat_006", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Infinity", "mphipster_overlays", "FM_Hip_M_Tat_011", "FM_Hip_F_Tat_011", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Antlers", "mphipster_overlays", "FM_Hip_M_Tat_012", "FM_Hip_F_Tat_012", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Boombox", "mphipster_overlays", "FM_Hip_M_Tat_013", "FM_Hip_F_Tat_013", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Pyramid", "mphipster_overlays", "FM_Hip_M_Tat_024", "FM_Hip_F_Tat_024", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Watch Your Step", "mphipster_overlays", "FM_Hip_M_Tat_025", "FM_Hip_F_Tat_025", 150),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Sad", "mphipster_overlays", "FM_Hip_M_Tat_029", "FM_Hip_F_Tat_029", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Shark Fin", "mphipster_overlays", "FM_Hip_M_Tat_030", "FM_Hip_F_Tat_030", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Skateboard", "mphipster_overlays", "FM_Hip_M_Tat_031", "FM_Hip_F_Tat_031", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Paper Plane", "mphipster_overlays", "FM_Hip_M_Tat_032", "FM_Hip_F_Tat_032", 150),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Stag", "mphipster_overlays", "FM_Hip_M_Tat_033", "FM_Hip_F_Tat_033", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Sewn Heart", "mphipster_overlays", "FM_Hip_M_Tat_035", "FM_Hip_F_Tat_035", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Tooth", "mphipster_overlays", "FM_Hip_M_Tat_041", "FM_Hip_F_Tat_041", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Triangles", "mphipster_overlays", "FM_Hip_M_Tat_046", "FM_Hip_F_Tat_046", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Cassette", "mphipster_overlays", "FM_Hip_M_Tat_047", "FM_Hip_F_Tat_047", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Block Back", "mpimportexport_overlays", "MP_MP_ImportExport_Tat_000_M", "MP_MP_ImportExport_Tat_000_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Power Plant", "mpimportexport_overlays", "MP_MP_ImportExport_Tat_001_M", "MP_MP_ImportExport_Tat_001_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Tuned to Death", "mpimportexport_overlays", "MP_MP_ImportExport_Tat_002_M", "MP_MP_ImportExport_Tat_002_F", 350),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Serpents of Destruction", "mpimportexport_overlays", "MP_MP_ImportExport_Tat_009_M", "MP_MP_ImportExport_Tat_009_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Take the Wheel", "mpimportexport_overlays", "MP_MP_ImportExport_Tat_010_M", "MP_MP_ImportExport_Tat_010_F", 350),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Talk Shit Get Hit", "mpimportexport_overlays", "MP_MP_ImportExport_Tat_011_M", "MP_MP_ImportExport_Tat_011_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "King Fight", "mplowrider_overlays", "MP_LR_Tat_001_M", "MP_LR_Tat_001_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Holy Mary", "mplowrider_overlays", "MP_LR_Tat_002_M", "MP_LR_Tat_002_F", 350),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Gun Mic", "mplowrider_overlays", "MP_LR_Tat_004_M", "MP_LR_Tat_004_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Amazon", "mplowrider_overlays", "MP_LR_Tat_009_M", "MP_LR_Tat_009_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Bad Angel", "mplowrider_overlays", "MP_LR_Tat_010_M", "MP_LR_Tat_010_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Love Gamble", "mplowrider_overlays", "MP_LR_Tat_013_M", "MP_LR_Tat_013_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Love is Blind", "mplowrider_overlays", "MP_LR_Tat_014_M", "MP_LR_Tat_014_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Sad Angel", "mplowrider_overlays", "MP_LR_Tat_021_M", "MP_LR_Tat_021_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Royal Takeover", "mplowrider_overlays", "MP_LR_Tat_026_M", "MP_LR_Tat_026_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Turbulence", "mpairraces_overlays", "MP_Airraces_Tattoo_000_M", "MP_Airraces_Tattoo_000_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Pilot Skull", "mpairraces_overlays", "MP_Airraces_Tattoo_001_M", "MP_Airraces_Tattoo_001_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Winged Bombshell", "mpairraces_overlays", "MP_Airraces_Tattoo_002_M", "MP_Airraces_Tattoo_002_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Balloon Pioneer", "mpairraces_overlays", "MP_Airraces_Tattoo_004_M", "MP_Airraces_Tattoo_004_F", 350),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Parachute Belle", "mpairraces_overlays", "MP_Airraces_Tattoo_005_M", "MP_Airraces_Tattoo_005_F", 350),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Bombs Away", "mpairraces_overlays", "MP_Airraces_Tattoo_006_M", "MP_Airraces_Tattoo_006_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Eagle Eyes", "mpairraces_overlays", "MP_Airraces_Tattoo_007_M", "MP_Airraces_Tattoo_007_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Demon Rider", "mpbiker_overlays", "MP_MP_Biker_Tat_000_M", "MP_MP_Biker_Tat_000_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Both Barrels", "mpbiker_overlays", "MP_MP_Biker_Tat_001_M", "MP_MP_Biker_Tat_001_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Web Rider", "mpbiker_overlays", "MP_MP_Biker_Tat_003_M", "MP_MP_Biker_Tat_003_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Made In America", "mpbiker_overlays", "MP_MP_Biker_Tat_005_M", "MP_MP_Biker_Tat_005_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Chopper Freedom", "mpbiker_overlays", "MP_MP_Biker_Tat_006_M", "MP_MP_Biker_Tat_006_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Freedom Wheels", "mpbiker_overlays", "MP_MP_Biker_Tat_008_M", "MP_MP_Biker_Tat_008_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Skull Of Taurus", "mpbiker_overlays", "MP_MP_Biker_Tat_010_M", "MP_MP_Biker_Tat_010_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "R.I.P. My Brothers", "mpbiker_overlays", "MP_MP_Biker_Tat_011_M", "MP_MP_Biker_Tat_011_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Demon Crossbones", "mpbiker_overlays", "MP_MP_Biker_Tat_013_M", "MP_MP_Biker_Tat_013_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Clawed Beast", "mpbiker_overlays", "MP_MP_Biker_Tat_017_M", "MP_MP_Biker_Tat_017_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Skeletal Chopper", "mpbiker_overlays", "MP_MP_Biker_Tat_018_M", "MP_MP_Biker_Tat_018_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Gruesome Talons", "mpbiker_overlays", "MP_MP_Biker_Tat_019_M", "MP_MP_Biker_Tat_019_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Flaming Reaper", "mpbiker_overlays", "MP_MP_Biker_Tat_021_M", "MP_MP_Biker_Tat_021_F", 350),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Western MC", "mpbiker_overlays", "MP_MP_Biker_Tat_023_M", "MP_MP_Biker_Tat_023_F", 350),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "American Dream", "mpbiker_overlays", "MP_MP_Biker_Tat_026_M", "MP_MP_Biker_Tat_026_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Bone Wrench", "mpbiker_overlays", "MP_MP_Biker_Tat_029_M", "MP_MP_Biker_Tat_029_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Brothers For Life", "mpbiker_overlays", "MP_MP_Biker_Tat_030_M", "MP_MP_Biker_Tat_030_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Gear Head", "mpbiker_overlays", "MP_MP_Biker_Tat_031_M", "MP_MP_Biker_Tat_031_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Western Eagle", "mpbiker_overlays", "MP_MP_Biker_Tat_032_M", "MP_MP_Biker_Tat_032_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Brotherhood of Bikes", "mpbiker_overlays", "MP_MP_Biker_Tat_034_M", "MP_MP_Biker_Tat_034_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Gas Guzzler", "mpbiker_overlays", "MP_MP_Biker_Tat_039_M", "MP_MP_Biker_Tat_039_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "No Regrets", "mpbiker_overlays", "MP_MP_Biker_Tat_041_M", "MP_MP_Biker_Tat_041_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Ride Forever", "mpbiker_overlays", "MP_MP_Biker_Tat_043_M", "MP_MP_Biker_Tat_043_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Unforgiven", "mpbiker_overlays", "MP_MP_Biker_Tat_050_M", "MP_MP_Biker_Tat_050_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Biker Mount", "mpbiker_overlays", "MP_MP_Biker_Tat_052_M", "MP_MP_Biker_Tat_052_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Reaper Vulture", "mpbiker_overlays", "MP_MP_Biker_Tat_058_M", "MP_MP_Biker_Tat_058_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Faggio", "mpbiker_overlays", "MP_MP_Biker_Tat_059_M", "MP_MP_Biker_Tat_059_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "We Are The Mods!", "mpbiker_overlays", "MP_MP_Biker_Tat_060_M", "MP_MP_Biker_Tat_060_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "SA Assault", "mplowrider2_overlays", "MP_LR_Tat_000_M", "MP_LR_Tat_000_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Love the Game", "mplowrider2_overlays", "MP_LR_Tat_008_M", "MP_LR_Tat_008_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Lady Liberty", "mplowrider2_overlays", "MP_LR_Tat_011_M", "MP_LR_Tat_011_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Royal Kiss", "mplowrider2_overlays", "MP_LR_Tat_012_M", "MP_LR_Tat_012_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Two Face", "mplowrider2_overlays", "MP_LR_Tat_016_M", "MP_LR_Tat_016_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Death Behind", "mplowrider2_overlays", "MP_LR_Tat_019_M", "MP_LR_Tat_019_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Dead Pretty", "mplowrider2_overlays", "MP_LR_Tat_031_M", "MP_LR_Tat_031_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Reign Over", "mplowrider2_overlays", "MP_LR_Tat_032_M", "MP_LR_Tat_032_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Abstract Skull", "mpluxe_overlays", "MP_LUXE_TAT_003_M", "MP_LUXE_TAT_003_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Eye of the Griffin", "mpluxe_overlays", "MP_LUXE_TAT_007_M", "MP_LUXE_TAT_007_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Flying Eye", "mpluxe_overlays", "MP_LUXE_TAT_008_M", "MP_LUXE_TAT_008_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Ancient Queen", "mpluxe_overlays", "MP_LUXE_TAT_014_M", "MP_LUXE_TAT_014_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Smoking Sisters", "mpluxe_overlays", "MP_LUXE_TAT_015_M", "MP_LUXE_TAT_015_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Feather Mural", "mpluxe_overlays", "MP_LUXE_TAT_024_M", "MP_LUXE_TAT_024_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "The Howler", "mpluxe2_overlays", "MP_LUXE_TAT_002_M", "MP_LUXE_TAT_002_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Geometric Galaxy", "mpluxe2_overlays", "MP_LUXE_TAT_012_M", "MP_LUXE_TAT_012_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Cloaked Angel", "mpluxe2_overlays", "MP_LUXE_TAT_022_M", "MP_LUXE_TAT_022_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Reaper Sway", "mpluxe2_overlays", "MP_LUXE_TAT_025_M", "MP_LUXE_TAT_025_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Cobra Dawn", "mpluxe2_overlays", "MP_LUXE_TAT_027_M", "MP_LUXE_TAT_027_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Geometric Design", "mpluxe2_overlays", "MP_LUXE_TAT_029_M", "MP_LUXE_TAT_029_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Bless The Dead", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_000_M", "MP_Smuggler_Tattoo_000_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Dead Lies", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_002_M", "MP_Smuggler_Tattoo_002_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Give Nothing Back", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_003_M", "MP_Smuggler_Tattoo_003_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Never Surrender", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_006_M", "MP_Smuggler_Tattoo_006_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "No Honor", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_007_M", "MP_Smuggler_Tattoo_007_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Tall Ship Conflict", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_009_M", "MP_Smuggler_Tattoo_009_F", 350),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "See You In Hell", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_010_M", "MP_Smuggler_Tattoo_010_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Torn Wings", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_013_M", "MP_Smuggler_Tattoo_013_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Jolly Roger", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_015_M", "MP_Smuggler_Tattoo_015_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Skull Compass", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_016_M", "MP_Smuggler_Tattoo_016_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Framed Tall Ship", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_017_M", "MP_Smuggler_Tattoo_017_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Finders Keepers", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_018_M", "MP_Smuggler_Tattoo_018_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Lost At Sea", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_019_M", "MP_Smuggler_Tattoo_019_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Dead Tales", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_021_M", "MP_Smuggler_Tattoo_021_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "X Marks The Spot", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_022_M", "MP_Smuggler_Tattoo_022_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Pirate Captain", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_024_M", "MP_Smuggler_Tattoo_024_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Claimed By The Beast", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_025_M", "MP_Smuggler_Tattoo_025_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Wheels of Death", "mpstunt_overlays", "MP_MP_Stunt_Tat_011_M", "MP_MP_Stunt_Tat_011_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Punk Biker", "mpstunt_overlays", "MP_MP_Stunt_Tat_012_M", "MP_MP_Stunt_Tat_012_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Bat Cat of Spades", "mpstunt_overlays", "MP_MP_Stunt_Tat_014_M", "MP_MP_Stunt_Tat_014_F", 350),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Vintage Bully", "mpstunt_overlays", "MP_MP_Stunt_Tat_018_M", "MP_MP_Stunt_Tat_018_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Engine Heart", "mpstunt_overlays", "MP_MP_Stunt_Tat_019_M", "MP_MP_Stunt_Tat_019_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Road Kill", "mpstunt_overlays", "MP_MP_Stunt_Tat_024_M", "MP_MP_Stunt_Tat_024_F", 350),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Winged Wheel", "mpstunt_overlays", "MP_MP_Stunt_Tat_026_M", "MP_MP_Stunt_Tat_026_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Punk Road Hog", "mpstunt_overlays", "MP_MP_Stunt_Tat_027_M", "MP_MP_Stunt_Tat_027_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Majestic Finish", "mpstunt_overlays", "MP_MP_Stunt_Tat_029_M", "MP_MP_Stunt_Tat_029_F", 350),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Man's Ruin", "mpstunt_overlays", "MP_MP_Stunt_Tat_030_M", "MP_MP_Stunt_Tat_030_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Sugar Skull Trucker", "mpstunt_overlays", "MP_MP_Stunt_Tat_033_M", "MP_MP_Stunt_Tat_033_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Feather Road Kill", "mpstunt_overlays", "MP_MP_Stunt_Tat_034_M", "MP_MP_Stunt_Tat_034_F", 350),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Big Grills", "mpstunt_overlays", "MP_MP_Stunt_Tat_037_M", "MP_MP_Stunt_Tat_037_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Monkey Chopper", "mpstunt_overlays", "MP_MP_Stunt_Tat_040_M", "MP_MP_Stunt_Tat_040_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Brapp", "mpstunt_overlays", "MP_MP_Stunt_Tat_041_M", "MP_MP_Stunt_Tat_041_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Ram Skull", "mpstunt_overlays", "MP_MP_Stunt_Tat_044_M", "MP_MP_Stunt_Tat_044_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Full Throttle", "mpstunt_overlays", "MP_MP_Stunt_Tat_046_M", "MP_MP_Stunt_Tat_046_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Racing Doll", "mpstunt_overlays", "MP_MP_Stunt_Tat_048_M", "MP_MP_Stunt_Tat_048_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Blackjack", "multiplayer_overlays", "FM_Tat_Award_M_003", "FM_Tat_Award_F_003", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Hustler", "multiplayer_overlays", "FM_Tat_Award_M_004", "FM_Tat_Award_F_004", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Angel", "multiplayer_overlays", "FM_Tat_Award_M_005", "FM_Tat_Award_F_005", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Los Santos Customs", "multiplayer_overlays", "FM_Tat_Award_M_008", "FM_Tat_Award_F_008", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Blank Scroll", "multiplayer_overlays", "FM_Tat_Award_M_011", "FM_Tat_Award_F_011", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Embellished Scroll", "multiplayer_overlays", "FM_Tat_Award_M_012", "FM_Tat_Award_F_012", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Seven Deadly Sins", "multiplayer_overlays", "FM_Tat_Award_M_013", "FM_Tat_Award_F_013", 150),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Trust No One", "multiplayer_overlays", "FM_Tat_Award_M_014", "FM_Tat_Award_F_014", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Clown", "multiplayer_overlays", "FM_Tat_Award_M_016", "FM_Tat_Award_F_016", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Clown and Gun", "multiplayer_overlays", "FM_Tat_Award_M_017", "FM_Tat_Award_F_017", 220),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Clown Dual Wield", "multiplayer_overlays", "FM_Tat_Award_M_018", "FM_Tat_Award_F_018", 240),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Clown Dual Wield Dollars", "multiplayer_overlays", "FM_Tat_Award_M_019", "FM_Tat_Award_F_019", 260),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Faith", "multiplayer_overlays", "FM_Tat_M_004", "FM_Tat_F_004", 350),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Skull on the Cross", "multiplayer_overlays", "FM_Tat_M_009", "FM_Tat_F_009", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "LS Flames", "multiplayer_overlays", "FM_Tat_M_010", "FM_Tat_F_010", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "LS Script", "multiplayer_overlays", "FM_Tat_M_011", "FM_Tat_F_011", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Los Santos Bills", "multiplayer_overlays", "FM_Tat_M_012", "FM_Tat_F_012", 350),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Eagle and Serpent", "multiplayer_overlays", "FM_Tat_M_013", "FM_Tat_F_013", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Evil Clown", "multiplayer_overlays", "FM_Tat_M_016", "FM_Tat_F_016", 450),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "The Wages of Sin", "multiplayer_overlays", "FM_Tat_M_019", "FM_Tat_F_019", 450),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Dragon", "multiplayer_overlays", "FM_Tat_M_020", "FM_Tat_F_020", 420),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Flaming Cross", "multiplayer_overlays", "FM_Tat_M_024", "FM_Tat_F_024", 350),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "LS Bold", "multiplayer_overlays", "FM_Tat_M_025", "FM_Tat_F_025", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Trinity Knot", "multiplayer_overlays", "FM_Tat_M_029", "FM_Tat_F_029", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Lucky Celtic Dogs", "multiplayer_overlays", "FM_Tat_M_030", "FM_Tat_F_030", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Flaming Shamrock", "multiplayer_overlays", "FM_Tat_M_034", "FM_Tat_F_034", 150),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Way of the Gun", "multiplayer_overlays", "FM_Tat_M_036", "FM_Tat_F_036", 250),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Stone Cross", "multiplayer_overlays", "FM_Tat_M_044", "FM_Tat_F_044", 250),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Skulls and Rose", "multiplayer_overlays", "FM_Tat_M_045", "FM_Tat_F_045", 400),
            
            // Head
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Cash is King", "mpbusiness_overlays", "MP_Buis_M_Neck_000", string.Empty, 100),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Bold Dollar Sign", "mpbusiness_overlays", "MP_Buis_M_Neck_001", string.Empty, 100),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Script Dollar Sign", "mpbusiness_overlays", "MP_Buis_M_Neck_002", string.Empty, 100),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "$100", "mpbusiness_overlays", "MP_Buis_M_Neck_003", string.Empty, 150),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Val-de-Grace Logo", "mpbusiness_overlays", string.Empty, "MP_Buis_F_Neck_000", 100),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Money Rose", "mpbusiness_overlays", string.Empty, "MP_Buis_F_Neck_001", 100),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Los Muertos", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_007", "MP_Xmas2_F_Tat_007", 150),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Snake Head Outline", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_024", "MP_Xmas2_F_Tat_024", 100),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Snake Head Color", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_025", "MP_Xmas2_F_Tat_025", 150),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Beautiful Death", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_029", "MP_Xmas2_F_Tat_029", 150),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Lock & Load", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_003_M", "MP_Gunrunning_Tattoo_003_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Beautiful Eye", "mphipster_overlays", "FM_Hip_M_Tat_005", "FM_Hip_F_Tat_005", 100),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Geo Fox", "mphipster_overlays", "FM_Hip_M_Tat_021", "FM_Hip_F_Tat_021", 100),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Morbid Arachnid", "mpbiker_overlays", "MP_MP_Biker_Tat_009_M", "MP_MP_Biker_Tat_009_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "FTW", "mpbiker_overlays", "MP_MP_Biker_Tat_038_M", "MP_MP_Biker_Tat_038_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Western Stylized", "mpbiker_overlays", "MP_MP_Biker_Tat_051_M", "MP_MP_Biker_Tat_051_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Sinner", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_011_M", "MP_Smuggler_Tattoo_011_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Thief", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_012_M", "MP_Smuggler_Tattoo_012_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Stunt Skull", "mpstunt_overlays", "MP_MP_Stunt_Tat_000_M", "MP_MP_Stunt_Tat_000_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Scorpion", "mpstunt_overlays", "MP_MP_Stunt_Tat_004_M", "MP_MP_Stunt_Tat_004_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Toxic Spider", "mpstunt_overlays", "MP_MP_Stunt_Tat_006_M", "MP_MP_Stunt_Tat_006_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Bat Wheel", "mpstunt_overlays", "MP_MP_Stunt_Tat_017_M", "MP_MP_Stunt_Tat_017_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Flaming Quad", "mpstunt_overlays", "MP_MP_Stunt_Tat_042_M", "MP_MP_Stunt_Tat_042_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Skull", "multiplayer_overlays", "FM_Tat_Award_M_000", "FM_Tat_Award_F_000", 100),

            // Left arm
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "$100 Bill", "mpbusiness_overlays", "MP_Buis_M_LeftArm_000", string.Empty, 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "All-Seeing Eye", "mpbusiness_overlays", "MP_Buis_M_LeftArm_001", string.Empty, 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Greed is Good", "mpbusiness_overlays", string.Empty, "MP_Buis_F_LArm_000", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Skull Rider", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_000", "MP_Xmas2_F_Tat_000", 250),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Electric Snake", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_010", "MP_Xmas2_F_Tat_010", 250),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "8 Ball Skull", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_012", "MP_Xmas2_F_Tat_012", 250),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Time's Up Outline", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_020", "MP_Xmas2_F_Tat_020", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Time's Up Color", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_021", "MP_Xmas2_F_Tat_021", 150),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Sidearm", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_004_M", "MP_Gunrunning_Tattoo_004_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Bandolier", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_008_M", "MP_Gunrunning_Tattoo_008_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Spiked Skull", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_015_M", "MP_Gunrunning_Tattoo_015_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Blood Money", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_016_M", "MP_Gunrunning_Tattoo_016_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Praying Skull", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_025_M", "MP_Gunrunning_Tattoo_025_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Serpent Revolver", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_027_M", "MP_Gunrunning_Tattoo_027_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Diamond Sparkle", "mphipster_overlays", "FM_Hip_M_Tat_003", "FM_Hip_F_Tat_003", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Bricks", "mphipster_overlays", "FM_Hip_M_Tat_007", "FM_Hip_F_Tat_007", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Mustache", "mphipster_overlays", "FM_Hip_M_Tat_015", "FM_Hip_F_Tat_015", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Lightning Bolt", "mphipster_overlays", "FM_Hip_M_Tat_016", "FM_Hip_F_Tat_016", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Pizza", "mphipster_overlays", "FM_Hip_M_Tat_026", "FM_Hip_F_Tat_026", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Padlock", "mphipster_overlays", "FM_Hip_M_Tat_027", "FM_Hip_F_Tat_027", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Thorny Rose", "mphipster_overlays", "FM_Hip_M_Tat_028", "FM_Hip_F_Tat_028", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Stop", "mphipster_overlays", "FM_Hip_M_Tat_034", "FM_Hip_F_Tat_034", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Sunrise", "mphipster_overlays", "FM_Hip_M_Tat_037", "FM_Hip_F_Tat_037", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Sleeve", "mphipster_overlays", "FM_Hip_M_Tat_039", "FM_Hip_F_Tat_039", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Triangle White", "mphipster_overlays", "FM_Hip_M_Tat_043", "FM_Hip_F_Tat_043", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Peace", "mphipster_overlays", "FM_Hip_M_Tat_048", "FM_Hip_F_Tat_048", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Piston Sleeve", "mpimportexport_overlays", "MP_MP_ImportExport_Tat_004_M", "MP_MP_ImportExport_Tat_004_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Scarlett", "mpimportexport_overlays", "MP_MP_ImportExport_Tat_008_M", "MP_MP_ImportExport_Tat_008_F", 350),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "No Evil", "mplowrider_overlays", "MP_LR_Tat_005_M", "MP_LR_Tat_005_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Los Santos Life", "mplowrider_overlays", "MP_LR_Tat_027_M", "MP_LR_Tat_027_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "City Sorrow", "mplowrider_overlays", "MP_LR_Tat_033_M", "MP_LR_Tat_033_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Toxic Trails", "mpairraces_overlays", "MP_Airraces_Tattoo_003_M", "MP_Airraces_Tattoo_003_F", 350),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Urban Stunter", "mpbiker_overlays", "MP_MP_Biker_Tat_012_M", "MP_MP_Biker_Tat_012_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Macabre Tree", "mpbiker_overlays", "MP_MP_Biker_Tat_016_M", "MP_MP_Biker_Tat_016_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Cranial Rose", "mpbiker_overlays", "MP_MP_Biker_Tat_020_M", "MP_MP_Biker_Tat_020_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Live to Ride", "mpbiker_overlays", "MP_MP_Biker_Tat_024_M", "MP_MP_Biker_Tat_024_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Good Luck", "mpbiker_overlays", "MP_MP_Biker_Tat_025_M", "MP_MP_Biker_Tat_025_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Chain Fist", "mpbiker_overlays", "MP_MP_Biker_Tat_035_M", "MP_MP_Biker_Tat_035_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Ride Hard Die Fast", "mpbiker_overlays", "MP_MP_Biker_Tat_045_M", "MP_MP_Biker_Tat_045_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Muffler Helmet", "mpbiker_overlays", "MP_MP_Biker_Tat_053_M", "MP_MP_Biker_Tat_053_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Poison Scorpion", "mpbiker_overlays", "MP_MP_Biker_Tat_055_M", "MP_MP_Biker_Tat_055_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Love Hustle", "mplowrider2_overlays", "MP_LR_Tat_006_M", "MP_LR_Tat_006_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Skeleton Party", "mplowrider2_overlays", "MP_LR_Tat_018_M", "MP_LR_Tat_018_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "My Crazy Life", "mplowrider2_overlays", "MP_LR_Tat_022_M", "MP_LR_Tat_022_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Archangel & Mary", "mpluxe_overlays", "MP_LUXE_TAT_020_M", "MP_LUXE_TAT_020_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Gabriel", "mpluxe_overlays", "MP_LUXE_TAT_021_M", "MP_LUXE_TAT_021_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Fatal Dagger", "mpluxe2_overlays", "MP_LUXE_TAT_005_M", "MP_LUXE_TAT_005_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Egyptian Mural", "mpluxe2_overlays", "MP_LUXE_TAT_016_M", "MP_LUXE_TAT_016_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Divine Goddess", "mpluxe2_overlays", "MP_LUXE_TAT_018_M", "MP_LUXE_TAT_018_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Python Skull", "mpluxe2_overlays", "MP_LUXE_TAT_028_M", "MP_LUXE_TAT_028_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Geometric Design", "mpluxe2_overlays", "MP_LUXE_TAT_031_M", "MP_LUXE_TAT_031_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Honor", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_004_M", "MP_Smuggler_Tattoo_004_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Horrors Of The Deep", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_008_M", "MP_Smuggler_Tattoo_008_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Mermaid's Curse", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_014_M", "MP_Smuggler_Tattoo_014_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "8 Eyed Skull", "mpstunt_overlays", "MP_MP_Stunt_Tat_001_M", "MP_MP_Stunt_Tat_001_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Big Cat", "mpstunt_overlays", "MP_MP_Stunt_Tat_002_M", "MP_MP_Stunt_Tat_002_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Moonlight Ride", "mpstunt_overlays", "MP_MP_Stunt_Tat_008_M", "MP_MP_Stunt_Tat_008_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Piston Head", "mpstunt_overlays", "MP_MP_Stunt_Tat_022_M", "MP_MP_Stunt_Tat_022_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Tanked", "mpstunt_overlays", "MP_MP_Stunt_Tat_023_M", "MP_MP_Stunt_Tat_023_F", 450),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Stuntman's End", "mpstunt_overlays", "MP_MP_Stunt_Tat_035_M", "MP_MP_Stunt_Tat_035_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Kaboom", "mpstunt_overlays", "MP_MP_Stunt_Tat_039_M", "MP_MP_Stunt_Tat_039_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Engine Arm", "mpstunt_overlays", "MP_MP_Stunt_Tat_043_M", "MP_MP_Stunt_Tat_043_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Burning Heart", "multiplayer_overlays", "FM_Tat_Award_M_001", "FM_Tat_Award_F_001", 150),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Racing Blonde", "multiplayer_overlays", "FM_Tat_Award_M_007", "FM_Tat_Award_F_007", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Racing Brunette", "multiplayer_overlays", "FM_Tat_Award_M_015", "FM_Tat_Award_F_015", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Serpents", "multiplayer_overlays", "FM_Tat_M_005", "FM_Tat_F_005", 150),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Oriental Mural", "multiplayer_overlays", "FM_Tat_M_006", "FM_Tat_F_006", 150),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Zodiac Skull", "multiplayer_overlays", "FM_Tat_M_015", "FM_Tat_F_015", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Lady M", "multiplayer_overlays", "FM_Tat_M_031", "FM_Tat_F_031", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Dope Skull", "multiplayer_overlays", "FM_Tat_M_041", "FM_Tat_F_041", 100),

            // Right arm
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Dollar Skull", "mpbusiness_overlays", "MP_Buis_M_RightArm_000", string.Empty, 150),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Green", "mpbusiness_overlays", "MP_Buis_M_RightArm_001", string.Empty, 150),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Dollar Sign", "mpbusiness_overlays", string.Empty, "MP_Buis_F_RArm_000", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Snake Outline", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_003", "MP_Xmas2_F_Tat_003", 150),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Snake Shaded", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_004", "MP_Xmas2_F_Tat_004", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Death Before Dishonor", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_008", "MP_Xmas2_F_Tat_008", 250),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "You're Next Outline", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_022", "MP_Xmas2_F_Tat_022", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "You're Next Color", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_023", "MP_Xmas2_F_Tat_023", 250),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Fuck Luck Outline", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_026", "MP_Xmas2_F_Tat_026", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Fuck Luck Color", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_027", "MP_Xmas2_F_Tat_027", 150),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Grenade", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_002_M", "MP_Gunrunning_Tattoo_002_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Have a Nice Day", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_021_M", "MP_Gunrunning_Tattoo_021_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Combat Reaper", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_024_M", "MP_Gunrunning_Tattoo_024_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Single Arrow", "mphipster_overlays", "FM_Hip_M_Tat_001", "FM_Hip_F_Tat_001", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Bone", "mphipster_overlays", "FM_Hip_M_Tat_004", "FM_Hip_F_Tat_004", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Cube", "mphipster_overlays", "FM_Hip_M_Tat_008", "FM_Hip_F_Tat_008", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Horseshoe", "mphipster_overlays", "FM_Hip_M_Tat_010", "FM_Hip_F_Tat_010", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Spray Can", "mphipster_overlays", "FM_Hip_M_Tat_014", "FM_Hip_F_Tat_014", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Eye Triangle", "mphipster_overlays", "FM_Hip_M_Tat_017", "FM_Hip_F_Tat_017", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Origami", "mphipster_overlays", "FM_Hip_M_Tat_018", "FM_Hip_F_Tat_018", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Geo Pattern", "mphipster_overlays", "FM_Hip_M_Tat_020", "FM_Hip_F_Tat_020", 150),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Pencil", "mphipster_overlays", "FM_Hip_M_Tat_022", "FM_Hip_F_Tat_022", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Smiley", "mphipster_overlays", "FM_Hip_M_Tat_023", "FM_Hip_F_Tat_023", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Shapes", "mphipster_overlays", "FM_Hip_M_Tat_036", "FM_Hip_F_Tat_036", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Triangle Black", "mphipster_overlays", "FM_Hip_M_Tat_044", "FM_Hip_F_Tat_044", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Mesh Band", "mphipster_overlays", "FM_Hip_M_Tat_045", "FM_Hip_F_Tat_045", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Mechanical Sleeve", "mpimportexport_overlays", "MP_MP_ImportExport_Tat_003_M", "MP_MP_ImportExport_Tat_003_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Dialed In", "mpimportexport_overlays", "MP_MP_ImportExport_Tat_005_M", "MP_MP_ImportExport_Tat_005_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Engulfed Block", "mpimportexport_overlays", "MP_MP_ImportExport_Tat_006_M", "MP_MP_ImportExport_Tat_006_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Drive Forever", "mpimportexport_overlays", "MP_MP_ImportExport_Tat_007_M", "MP_MP_ImportExport_Tat_007_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Seductress", "mplowrider_overlays", "MP_LR_Tat_015_M", "MP_LR_Tat_015_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Swooping Eagle", "mpbiker_overlays", "MP_MP_Biker_Tat_007_M", "MP_MP_Biker_Tat_007_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Lady Mortality", "mpbiker_overlays", "MP_MP_Biker_Tat_014_M", "MP_MP_Biker_Tat_014_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Eagle Emblem", "mpbiker_overlays", "MP_MP_Biker_Tat_033_M", "MP_MP_Biker_Tat_033_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Grim Rider", "mpbiker_overlays", "MP_MP_Biker_Tat_042_M", "MP_MP_Biker_Tat_042_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Skull Chain", "mpbiker_overlays", "MP_MP_Biker_Tat_046_M", "MP_MP_Biker_Tat_046_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Snake Bike", "mpbiker_overlays", "MP_MP_Biker_Tat_047_M", "MP_MP_Biker_Tat_047_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "These Colors Don't Run", "mpbiker_overlays", "MP_MP_Biker_Tat_049_M", "MP_MP_Biker_Tat_049_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Mum", "mpbiker_overlays", "MP_MP_Biker_Tat_054_M", "MP_MP_Biker_Tat_054_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Lady Vamp", "mplowrider2_overlays", "MP_LR_Tat_003_M", "MP_LR_Tat_003_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Loving Los Muertos", "mplowrider2_overlays", "MP_LR_Tat_028_M", "MP_LR_Tat_028_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Black Tears", "mplowrider2_overlays", "MP_LR_Tat_035_M", "MP_LR_Tat_035_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Floral Raven", "mpluxe_overlays", "MP_LUXE_TAT_004_M", "MP_LUXE_TAT_004_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Mermaid Harpist", "mpluxe_overlays", "MP_LUXE_TAT_013_M", "MP_LUXE_TAT_013_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Geisha Bloom", "mpluxe_overlays", "MP_LUXE_TAT_019_M", "MP_LUXE_TAT_019_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Intrometric", "mpluxe2_overlays", "MP_LUXE_TAT_010_M", "MP_LUXE_TAT_010_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Heavenly Deity", "mpluxe2_overlays", "MP_LUXE_TAT_017_M", "MP_LUXE_TAT_017_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Floral Print", "mpluxe2_overlays", "MP_LUXE_TAT_026_M", "MP_LUXE_TAT_026_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Geometric Design", "mpluxe2_overlays", "MP_LUXE_TAT_030_M", "MP_LUXE_TAT_030_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Crackshot", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_001_M", "MP_Smuggler_Tattoo_001_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Mutiny", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_005_M", "MP_Smuggler_Tattoo_005_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Stylized Kraken", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_023_M", "MP_Smuggler_Tattoo_023_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Poison Wrench", "mpstunt_overlays", "MP_MP_Stunt_Tat_003_M", "MP_MP_Stunt_Tat_003_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Arachnid of Death", "mpstunt_overlays", "MP_MP_Stunt_Tat_009_M", "MP_MP_Stunt_Tat_009_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Grave Vulture", "mpstunt_overlays", "MP_MP_Stunt_Tat_010_M", "MP_MP_Stunt_Tat_010_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Coffin Racer", "mpstunt_overlays", "MP_MP_Stunt_Tat_016_M", "MP_MP_Stunt_Tat_016_F", 350),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Biker Stallion", "mpstunt_overlays", "MP_MP_Stunt_Tat_036_M", "MP_MP_Stunt_Tat_036_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "One Down Five Up", "mpstunt_overlays", "MP_MP_Stunt_Tat_038_M", "MP_MP_Stunt_Tat_038_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Seductive Mechanic", "mpstunt_overlays", "MP_MP_Stunt_Tat_049_M", "MP_MP_Stunt_Tat_049_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Grim Reaper Smoking Gun", "multiplayer_overlays", "FM_Tat_Award_M_002", "FM_Tat_Award_F_002", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Ride or Die", "multiplayer_overlays", "FM_Tat_Award_M_010", "FM_Tat_Award_F_010", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Brotherhood", "multiplayer_overlays", "FM_Tat_M_000", "FM_Tat_F_000", 300),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Dragons", "multiplayer_overlays", "FM_Tat_M_001", "FM_Tat_F_001", 350),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Dragons and Skull", "multiplayer_overlays", "FM_Tat_M_003", "FM_Tat_F_003", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Flower Mural", "multiplayer_overlays", "FM_Tat_M_014", "FM_Tat_F_014", 300),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Serpent Skull", "multiplayer_overlays", "FM_Tat_M_018", "FM_Tat_F_018", 350),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Virgin Mary", "multiplayer_overlays", "FM_Tat_M_027", "FM_Tat_F_027", 300),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Mermaid", "multiplayer_overlays", "FM_Tat_M_028", "FM_Tat_F_028", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Dagger", "multiplayer_overlays", "FM_Tat_M_038", "FM_Tat_F_038", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Lion", "multiplayer_overlays", "FM_Tat_M_047", "FM_Tat_F_047", 100),

            // Left leg
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Single", "mpbusiness_overlays", string.Empty, "MP_Buis_F_LLeg_000", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Spider Outline", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_001", "MP_Xmas2_F_Tat_001", 300),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Spider Color", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_002", "MP_Xmas2_F_Tat_002", 350),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Patriot Skull", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_005_M", "MP_Gunrunning_Tattoo_005_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Stylized Tiger", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_007_M", "MP_Gunrunning_Tattoo_007_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Death Skull", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_011_M", "MP_Gunrunning_Tattoo_011_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Rose Revolver", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_023_M", "MP_Gunrunning_Tattoo_023_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Squares", "mphipster_overlays", "FM_Hip_M_Tat_009", "FM_Hip_F_Tat_009", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Charm", "mphipster_overlays", "FM_Hip_M_Tat_019", "FM_Hip_F_Tat_019", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Black Anchor", "mphipster_overlays", "FM_Hip_M_Tat_040", "FM_Hip_F_Tat_040", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "LS Serpent", "mplowrider_overlays", "MP_LR_Tat_007_M", "MP_LR_Tat_007_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Presidents", "mplowrider_overlays", "MP_LR_Tat_020_M", "MP_LR_Tat_020_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Rose Tribute", "mpbiker_overlays", "MP_MP_Biker_Tat_002_M", "MP_MP_Biker_Tat_002_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Ride or Die", "mpbiker_overlays", "MP_MP_Biker_Tat_015_M", "MP_MP_Biker_Tat_015_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Bad Luck", "mpbiker_overlays", "MP_MP_Biker_Tat_027_M", "MP_MP_Biker_Tat_027_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Engulfed Skull", "mpbiker_overlays", "MP_MP_Biker_Tat_036_M", "MP_MP_Biker_Tat_036_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Scorched Soul", "mpbiker_overlays", "MP_MP_Biker_Tat_037_M", "MP_MP_Biker_Tat_037_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Ride Free", "mpbiker_overlays", "MP_MP_Biker_Tat_044_M", "MP_MP_Biker_Tat_044_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Bone Cruiser", "mpbiker_overlays", "MP_MP_Biker_Tat_056_M", "MP_MP_Biker_Tat_056_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Laughing Skull", "mpbiker_overlays", "MP_MP_Biker_Tat_057_M", "MP_MP_Biker_Tat_057_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Death Us Do Part", "mplowrider2_overlays", "MP_LR_Tat_029_M", "MP_LR_Tat_029_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Serpent of Death", "mpluxe_overlays", "MP_LUXE_TAT_000_M", "MP_LUXE_TAT_000_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Cross of Roses", "mpluxe2_overlays", "MP_LUXE_TAT_011_M", "MP_LUXE_TAT_011_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Dagger Devil", "mpstunt_overlays", "MP_MP_Stunt_Tat_007_M", "MP_MP_Stunt_Tat_007_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Dirt Track Hero", "mpstunt_overlays", "MP_MP_Stunt_Tat_013_M", "MP_MP_Stunt_Tat_013_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Golden Cobra", "mpstunt_overlays", "MP_MP_Stunt_Tat_021_M", "MP_MP_Stunt_Tat_021_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Quad Goblin", "mpstunt_overlays", "MP_MP_Stunt_Tat_028_M", "MP_MP_Stunt_Tat_028_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Stunt Jesus", "mpstunt_overlays", "MP_MP_Stunt_Tat_031_M", "MP_MP_Stunt_Tat_031_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Dragon and Dagger", "multiplayer_overlays", "FM_Tat_Award_M_009", "FM_Tat_Award_F_009", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Melting Skull", "multiplayer_overlays", "FM_Tat_M_002", "FM_Tat_F_002", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Dragon Mural", "multiplayer_overlays", "FM_Tat_M_008", "FM_Tat_F_008", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Serpent Skull", "multiplayer_overlays", "FM_Tat_M_021", "FM_Tat_F_021", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Hottie", "multiplayer_overlays", "FM_Tat_M_023", "FM_Tat_F_023", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Smoking Dagger", "multiplayer_overlays", "FM_Tat_M_026", "FM_Tat_F_026", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Faith", "multiplayer_overlays", "FM_Tat_M_032", "FM_Tat_F_032", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Chinese Dragon", "multiplayer_overlays", "FM_Tat_M_033", "FM_Tat_F_033", 300),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Dragon", "multiplayer_overlays", "FM_Tat_M_035", "FM_Tat_F_035", 250),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Grim Reaper", "multiplayer_overlays", "FM_Tat_M_037", "FM_Tat_F_037", 200),
            
            // Right leg
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Diamond Crown", "mpbusiness_overlays", string.Empty, "MP_Buis_F_RLeg_000", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Floral Dagger", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_014", "MP_Xmas2_F_Tat_014", 250),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Combat Skull", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_006_M", "MP_Gunrunning_Tattoo_006_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Restless Skull", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_026_M", "MP_Gunrunning_Tattoo_026_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Pistol Ace", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_030_M", "MP_Gunrunning_Tattoo_030_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Grub", "mphipster_overlays", "FM_Hip_M_Tat_038", "FM_Hip_F_Tat_038", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Sparkplug", "mphipster_overlays", "FM_Hip_M_Tat_042", "FM_Hip_F_Tat_042", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Ink Me", "mplowrider_overlays", "MP_LR_Tat_017_M", "MP_LR_Tat_017_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Dance of Hearts", "mplowrider_overlays", "MP_LR_Tat_023_M", "MP_LR_Tat_023_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Dragon's Fury", "mpbiker_overlays", "MP_MP_Biker_Tat_004_M", "MP_MP_Biker_Tat_004_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Western Insignia", "mpbiker_overlays", "MP_MP_Biker_Tat_022_M", "MP_MP_Biker_Tat_022_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Dusk Rider", "mpbiker_overlays", "MP_MP_Biker_Tat_028_M", "MP_MP_Biker_Tat_028_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "American Made", "mpbiker_overlays", "MP_MP_Biker_Tat_040_M", "MP_MP_Biker_Tat_040_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "STFU", "mpbiker_overlays", "MP_MP_Biker_Tat_048_M", "MP_MP_Biker_Tat_048_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "San Andreas Prayer", "mplowrider2_overlays", "MP_LR_Tat_030_M", "MP_LR_Tat_030_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Elaborate Los Muertos", "mpluxe_overlays", "MP_LUXE_TAT_001_M", "MP_LUXE_TAT_001_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Starmetric", "mpluxe2_overlays", "MP_LUXE_TAT_023_M", "MP_LUXE_TAT_023_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Homeward Bound", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_020_M", "MP_Smuggler_Tattoo_020_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Demon Spark Plug", "mpstunt_overlays", "MP_MP_Stunt_Tat_005_M", "MP_MP_Stunt_Tat_005_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Praying Gloves", "mpstunt_overlays", "MP_MP_Stunt_Tat_015_M", "MP_MP_Stunt_Tat_015_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Piston Angel", "mpstunt_overlays", "MP_MP_Stunt_Tat_020_M", "MP_MP_Stunt_Tat_020_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Speed Freak", "mpstunt_overlays", "MP_MP_Stunt_Tat_025_M", "MP_MP_Stunt_Tat_025_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Wheelie Mouse", "mpstunt_overlays", "MP_MP_Stunt_Tat_032_M", "MP_MP_Stunt_Tat_032_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Severed Hand", "mpstunt_overlays", "MP_MP_Stunt_Tat_045_M", "MP_MP_Stunt_Tat_045_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Brake Knife", "mpstunt_overlays", "MP_MP_Stunt_Tat_047_M", "MP_MP_Stunt_Tat_047_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Skull and Sword", "multiplayer_overlays", "FM_Tat_Award_M_006", "FM_Tat_Award_F_006", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "The Warrior", "multiplayer_overlays", "FM_Tat_M_007", "FM_Tat_F_007", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Tribal", "multiplayer_overlays", "FM_Tat_M_017", "FM_Tat_F_017", 250),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Fiery Dragon", "multiplayer_overlays", "FM_Tat_M_022", "FM_Tat_F_022", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Broken Skull", "multiplayer_overlays", "FM_Tat_M_039", "FM_Tat_F_039", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Flaming Skull", "multiplayer_overlays", "FM_Tat_M_040", "FM_Tat_F_040", 300),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Flaming Scorpion", "multiplayer_overlays", "FM_Tat_M_042", "FM_Tat_F_042", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Indian Ram", "multiplayer_overlays", "FM_Tat_M_043", "FM_Tat_F_043", 200)
        };

        // Car dealer's vehicles
        public static List<CarShopVehicleModel> CARSHOP_VEHICLE_LIST = new List<CarShopVehicleModel>
        {
            // Compacts
            new CarShopVehicleModel("Blista", VehicleHash.Blista, 0, VEHICLE_CLASS_COMPACTS, 10000),
            new CarShopVehicleModel("Blista2", VehicleHash.Blista2, 0, VEHICLE_CLASS_COMPACTS, 6000),
            new CarShopVehicleModel("Blista3", VehicleHash.Blista3, 0, VEHICLE_CLASS_COMPACTS, 30000),
            new CarShopVehicleModel("Brioso", VehicleHash.Brioso, 0, VEHICLE_CLASS_COMPACTS, 15000),
            new CarShopVehicleModel("Dilettante", VehicleHash.Dilettante, 0, VEHICLE_CLASS_COMPACTS, 7000),
            new CarShopVehicleModel("Issi2", VehicleHash.Issi2, 0, VEHICLE_CLASS_COMPACTS, 8000),
            new CarShopVehicleModel("Panto", VehicleHash.Panto, 0, VEHICLE_CLASS_COMPACTS, 5000),
            new CarShopVehicleModel("Prairie", VehicleHash.Prairie, 0, VEHICLE_CLASS_COMPACTS, 12000),
            new CarShopVehicleModel("Rhapsody", VehicleHash.Rhapsody, 0, VEHICLE_CLASS_COMPACTS, 1500),

            // Coupes
            new CarShopVehicleModel("CogCabrio", VehicleHash.CogCabrio, 0, VEHICLE_CLASS_COUPES, 39000),
            new CarShopVehicleModel("Exemplar", VehicleHash.Exemplar, 0, VEHICLE_CLASS_COUPES, 45000),
            new CarShopVehicleModel("F620", VehicleHash.F620, 0, VEHICLE_CLASS_COUPES, 46300),
            new CarShopVehicleModel("Felon", VehicleHash.Felon, 0, VEHICLE_CLASS_COUPES, 42000),
            new CarShopVehicleModel("Felon2", VehicleHash.Felon2, 0, VEHICLE_CLASS_COUPES, 42500),
            new CarShopVehicleModel("Jackal", VehicleHash.Jackal, 0, VEHICLE_CLASS_COUPES, 43200),
            new CarShopVehicleModel("Oracle", VehicleHash.Oracle, 0, VEHICLE_CLASS_COUPES, 28000),
            new CarShopVehicleModel("Oracle2", VehicleHash.Oracle2, 0, VEHICLE_CLASS_COUPES, 38000),
            new CarShopVehicleModel("Sentinel", VehicleHash.Sentinel, 0, VEHICLE_CLASS_COUPES, 25000),
            new CarShopVehicleModel("Sentinel2", VehicleHash.Sentinel2, 0, VEHICLE_CLASS_COUPES, 30000),
            new CarShopVehicleModel("Windsor", VehicleHash.Windsor, 0, VEHICLE_CLASS_COUPES, 55000),
            new CarShopVehicleModel("Windsor2", VehicleHash.Windsor2, 0, VEHICLE_CLASS_COUPES, 63000),
            new CarShopVehicleModel("Zion", VehicleHash.Zion, 0, VEHICLE_CLASS_COUPES, 25000),
            new CarShopVehicleModel("Zion2", VehicleHash.Zion2, 0, VEHICLE_CLASS_COUPES, 32000),

            // Muscle
            new CarShopVehicleModel("Blade", VehicleHash.Blade, 0, VEHICLE_CLASS_MUSCLE, 8000),
            new CarShopVehicleModel("Buccaneer", VehicleHash.Buccaneer, 0, VEHICLE_CLASS_MUSCLE, 14000),
            new CarShopVehicleModel("Buccaneer2", VehicleHash.Buccaneer2, 0, VEHICLE_CLASS_MUSCLE, 45000),
            new CarShopVehicleModel("Chino", VehicleHash.Chino, 0, VEHICLE_CLASS_MUSCLE, 16000),
            new CarShopVehicleModel("Chino2", VehicleHash.Chino2, 0, VEHICLE_CLASS_MUSCLE, 48000),
            new CarShopVehicleModel("Dominator", VehicleHash.Dominator, 0, VEHICLE_CLASS_MUSCLE, 30000),
            new CarShopVehicleModel("Dominator2", VehicleHash.Dominator2, 0, VEHICLE_CLASS_MUSCLE, 43000),
            new CarShopVehicleModel("Dukes", VehicleHash.Dukes, 0, VEHICLE_CLASS_MUSCLE, 15000),
            new CarShopVehicleModel("Faction", VehicleHash.Faction, 0, VEHICLE_CLASS_MUSCLE, 13000),
            new CarShopVehicleModel("Faction2", VehicleHash.Faction2, 0, VEHICLE_CLASS_MUSCLE, 43200),
            new CarShopVehicleModel("Faction3", VehicleHash.Faction3, 0, VEHICLE_CLASS_MUSCLE, 52500),
            new CarShopVehicleModel("Gauntlet", VehicleHash.Gauntlet, 0, VEHICLE_CLASS_MUSCLE, 28000),
            new CarShopVehicleModel("Gauntlet2", VehicleHash.Gauntlet2, 0, VEHICLE_CLASS_MUSCLE, 36000),
            new CarShopVehicleModel("Hotknife", VehicleHash.Hotknife, 0, VEHICLE_CLASS_MUSCLE, 150000),
            new CarShopVehicleModel("Moonbeam", VehicleHash.Moonbeam, 0, VEHICLE_CLASS_MUSCLE, 9000),
            new CarShopVehicleModel("Moonbeam2", VehicleHash.Moonbeam2, 0, VEHICLE_CLASS_MUSCLE, 36000),
            new CarShopVehicleModel("Nightshade", VehicleHash.Nightshade, 0, VEHICLE_CLASS_MUSCLE, 33000),
            new CarShopVehicleModel("Phoenix", VehicleHash.Phoenix, 0, VEHICLE_CLASS_MUSCLE, 20900),
            new CarShopVehicleModel("Picador", VehicleHash.Picador, 0, VEHICLE_CLASS_MUSCLE, 23000),
            new CarShopVehicleModel("RatLoader", VehicleHash.RatLoader, 0, VEHICLE_CLASS_MUSCLE, 3000),
            new CarShopVehicleModel("RatLoader2", VehicleHash.RatLoader2, 0, VEHICLE_CLASS_MUSCLE, 5000),
            new CarShopVehicleModel("Ruiner", VehicleHash.Ruiner, 0, VEHICLE_CLASS_MUSCLE, 23000),
            new CarShopVehicleModel("SabreGT", VehicleHash.SabreGT, 0, VEHICLE_CLASS_MUSCLE, 15500),
            new CarShopVehicleModel("SabreGT2", VehicleHash.SabreGT2, 0, VEHICLE_CLASS_MUSCLE, 49500),
            new CarShopVehicleModel("Sadler", VehicleHash.Sadler, 0, VEHICLE_CLASS_MUSCLE, 10000),
            new CarShopVehicleModel("SlamVan", VehicleHash.SlamVan, 0, VEHICLE_CLASS_MUSCLE, 11000),
            new CarShopVehicleModel("SlamVan2", VehicleHash.SlamVan2, 0, VEHICLE_CLASS_MUSCLE, 70000),
            new CarShopVehicleModel("SlamVan3", VehicleHash.SlamVan3, 0, VEHICLE_CLASS_MUSCLE, 42300),
            new CarShopVehicleModel("Stalion", VehicleHash.Stalion, 0, VEHICLE_CLASS_MUSCLE, 8700),
            new CarShopVehicleModel("Stalion2", VehicleHash.Stalion2, 0, VEHICLE_CLASS_MUSCLE, 38000),
            new CarShopVehicleModel("Tampa", VehicleHash.Tampa, 0, VEHICLE_CLASS_MUSCLE, 19000),
            new CarShopVehicleModel("Vigero", VehicleHash.Vigero, 0, VEHICLE_CLASS_MUSCLE, 10600),
            new CarShopVehicleModel("Virgo", VehicleHash.Virgo, 0, VEHICLE_CLASS_MUSCLE, 14700),
            new CarShopVehicleModel("Virgo2", VehicleHash.Virgo2, 0, VEHICLE_CLASS_MUSCLE, 48800),
            new CarShopVehicleModel("Virgo3", VehicleHash.Virgo3, 0, VEHICLE_CLASS_MUSCLE, 95000),
            new CarShopVehicleModel("Voodoo", VehicleHash.Voodoo, 0, VEHICLE_CLASS_MUSCLE, 32600),
            new CarShopVehicleModel("Voodoo2", VehicleHash.Voodoo2, 0, VEHICLE_CLASS_MUSCLE, 45000),

            // Off-Road
            new CarShopVehicleModel("BfInjection", VehicleHash.BfInjection, 0, VEHICLE_CLASS_OFFROAD, 36000),
            new CarShopVehicleModel("Bodhi2", VehicleHash.Bodhi2, 0, VEHICLE_CLASS_OFFROAD, 23000),
            new CarShopVehicleModel("Brawler", VehicleHash.Brawler, 0, VEHICLE_CLASS_OFFROAD, 72600),
            new CarShopVehicleModel("DLoader", VehicleHash.DLoader, 0, VEHICLE_CLASS_OFFROAD, 40000),
            new CarShopVehicleModel("Kalahari", VehicleHash.Kalahari, 0, VEHICLE_CLASS_OFFROAD, 16200),
            new CarShopVehicleModel("Mesa", VehicleHash.Mesa, 0, VEHICLE_CLASS_OFFROAD, 17200),
            new CarShopVehicleModel("RancherXL", VehicleHash.RancherXL, 0, VEHICLE_CLASS_OFFROAD, 15200),
            new CarShopVehicleModel("Rebel", VehicleHash.Rebel, 0, VEHICLE_CLASS_OFFROAD, 2700),
            new CarShopVehicleModel("Rebel2", VehicleHash.Rebel2, 0, VEHICLE_CLASS_OFFROAD, 10000),
            new CarShopVehicleModel("Sandking", VehicleHash.Sandking, 0, VEHICLE_CLASS_OFFROAD, 21000),
            new CarShopVehicleModel("Sandking2", VehicleHash.Sandking2, 0, VEHICLE_CLASS_OFFROAD, 20100),
            new CarShopVehicleModel("Blazer", VehicleHash.Blazer, 1, VEHICLE_CLASS_OFFROAD, 23000),
            new CarShopVehicleModel("Blazer2", VehicleHash.Blazer2, 1, VEHICLE_CLASS_OFFROAD, 16000),
            new CarShopVehicleModel("Blazer3", VehicleHash.Blazer3, 1, VEHICLE_CLASS_OFFROAD, 21000),
            new CarShopVehicleModel("Mesa3", VehicleHash.Mesa3, 0, VEHICLE_CLASS_OFFROAD, 35000),

            // SUVs
            new CarShopVehicleModel("BJXL", VehicleHash.BJXL, 0, VEHICLE_CLASS_SUVS, 18000),
            new CarShopVehicleModel("Baller", VehicleHash.Baller, 0, VEHICLE_CLASS_SUVS, 35000),
            new CarShopVehicleModel("Baller2", VehicleHash.Baller2, 0, VEHICLE_CLASS_SUVS, 39000),
            new CarShopVehicleModel("Baller3", VehicleHash.Baller3, 0, VEHICLE_CLASS_SUVS, 41000),
            new CarShopVehicleModel("Cavalcade", VehicleHash.Cavalcade, 0, VEHICLE_CLASS_SUVS, 16200),
            new CarShopVehicleModel("Cavalcade2", VehicleHash.Cavalcade2, 0, VEHICLE_CLASS_SUVS, 15900),
            new CarShopVehicleModel("Contender", VehicleHash.Contender, 0, VEHICLE_CLASS_SUVS, 43200),
            new CarShopVehicleModel("Dubsta", VehicleHash.Dubsta, 0, VEHICLE_CLASS_SUVS, 37600),
            new CarShopVehicleModel("Dubsta2", VehicleHash.Dubsta2, 0, VEHICLE_CLASS_SUVS, 46000),
            new CarShopVehicleModel("Dubsta3", VehicleHash.Dubsta3, 0, VEHICLE_CLASS_SUVS, 59000),
            new CarShopVehicleModel("FQ2", VehicleHash.FQ2, 0, VEHICLE_CLASS_SUVS, 38800),
            new CarShopVehicleModel("Granger", VehicleHash.Granger, 0, VEHICLE_CLASS_SUVS, 25000),
            new CarShopVehicleModel("Gresley", VehicleHash.Gresley, 0, VEHICLE_CLASS_SUVS, 26000),
            new CarShopVehicleModel("Habanero", VehicleHash.Habanero, 0, VEHICLE_CLASS_SUVS, 13900),
            new CarShopVehicleModel("Huntley", VehicleHash.Huntley, 0, VEHICLE_CLASS_SUVS, 43100),
            new CarShopVehicleModel("Landstalker", VehicleHash.Landstalker, 0, VEHICLE_CLASS_SUVS, 22000),
            new CarShopVehicleModel("Patriot", VehicleHash.Patriot, 0, VEHICLE_CLASS_SUVS, 26700),
            new CarShopVehicleModel("Radi", VehicleHash.Radi, 0, VEHICLE_CLASS_SUVS, 22000),
            new CarShopVehicleModel("Rocoto", VehicleHash.Rocoto, 0, VEHICLE_CLASS_SUVS, 31000),
            new CarShopVehicleModel("Seminole", VehicleHash.Seminole, 0, VEHICLE_CLASS_SUVS, 23700),
            new CarShopVehicleModel("Serrano", VehicleHash.Serrano, 0, VEHICLE_CLASS_SUVS, 22600),
            new CarShopVehicleModel("XLS", VehicleHash.XLS, 0, VEHICLE_CLASS_SUVS, 42500),
            new CarShopVehicleModel("Rumpo", VehicleHash.Rumpo, 0, VEHICLE_CLASS_SUVS, 35000),
            new CarShopVehicleModel("Rumpo3", VehicleHash.Rumpo3, 0, VEHICLE_CLASS_SUVS, 90000),
            new CarShopVehicleModel("GBurrito", VehicleHash.GBurrito, 0, VEHICLE_CLASS_SUVS, 36000),
            new CarShopVehicleModel("Burrito3", VehicleHash.Burrito3, 0, VEHICLE_CLASS_SUVS, 35000),
            new CarShopVehicleModel("Speedo", VehicleHash.Speedo, 0, VEHICLE_CLASS_SUVS, 23000),
            new CarShopVehicleModel("Pony", VehicleHash.Pony, 0, VEHICLE_CLASS_SUVS, 21000),
            new CarShopVehicleModel("GBurrito2", VehicleHash.GBurrito2, 0, VEHICLE_CLASS_SUVS, 32000),
            new CarShopVehicleModel("Bison", VehicleHash.Bison, 0, VEHICLE_CLASS_SUVS, 31000),
            new CarShopVehicleModel("Surfer", VehicleHash.Surfer, 0, VEHICLE_CLASS_SUVS, 15000),
            new CarShopVehicleModel("Surfer2", VehicleHash.Surfer2, 0, VEHICLE_CLASS_SUVS, 10000),
            new CarShopVehicleModel("Pony", VehicleHash.Pony, 0, VEHICLE_CLASS_SUVS, 20000),
            new CarShopVehicleModel("Mule3", VehicleHash.Mule3, 0, VEHICLE_CLASS_SUVS, 62000),
            new CarShopVehicleModel("Benson", VehicleHash.Benson, 0, VEHICLE_CLASS_SUVS, 76000),
            new CarShopVehicleModel("Guardian", VehicleHash.Guardian, 0, VEHICLE_CLASS_SUVS, 84000),
            new CarShopVehicleModel("Journey", VehicleHash.Journey, 0, VEHICLE_CLASS_SUVS, 21000),
            new CarShopVehicleModel("Camper", VehicleHash.Camper, 0, VEHICLE_CLASS_SUVS, 42000),

            // Sedans
            new CarShopVehicleModel("Asea", VehicleHash.Asea, 0, VEHICLE_CLASS_SEDANS, 7800),
            new CarShopVehicleModel("Asterope", VehicleHash.Asterope, 0, VEHICLE_CLASS_SEDANS, 15000),
            new CarShopVehicleModel("Cog55", VehicleHash.Cog55, 0, VEHICLE_CLASS_SEDANS, 55000),
            new CarShopVehicleModel("Cognoscenti", VehicleHash.Cognoscenti, 0, VEHICLE_CLASS_SEDANS, 65000),
            new CarShopVehicleModel("Emperor", VehicleHash.Emperor, 0, VEHICLE_CLASS_SEDANS, 6400),
            new CarShopVehicleModel("Emperor2", VehicleHash.Emperor2, 0, VEHICLE_CLASS_SEDANS, 3100),
            new CarShopVehicleModel("Fugitive", VehicleHash.Fugitive, 0, VEHICLE_CLASS_SEDANS, 14400),
            new CarShopVehicleModel("Glendale", VehicleHash.Glendale, 0, VEHICLE_CLASS_SEDANS, 12000),
            new CarShopVehicleModel("Ingot", VehicleHash.Ingot, 0, VEHICLE_CLASS_SEDANS, 5600),
            new CarShopVehicleModel("Intruder", VehicleHash.Intruder, 0, VEHICLE_CLASS_SEDANS, 7600),
            new CarShopVehicleModel("Premier", VehicleHash.Premier, 0, VEHICLE_CLASS_SEDANS, 11100),
            new CarShopVehicleModel("Primo", VehicleHash.Primo, 0, VEHICLE_CLASS_SEDANS, 13600),
            new CarShopVehicleModel("Primo2", VehicleHash.Primo2, 0, VEHICLE_CLASS_SEDANS, 46400),
            new CarShopVehicleModel("Regina", VehicleHash.Regina, 0, VEHICLE_CLASS_SEDANS, 4600),
            new CarShopVehicleModel("Stanier", VehicleHash.Stanier, 0, VEHICLE_CLASS_SEDANS, 9700),
            new CarShopVehicleModel("Surge", VehicleHash.Surge, 0, VEHICLE_CLASS_SEDANS, 13700),
            new CarShopVehicleModel("Tailgater", VehicleHash.Tailgater, 0, VEHICLE_CLASS_SEDANS, 33000),
            new CarShopVehicleModel("Warrener", VehicleHash.Warrener, 0, VEHICLE_CLASS_SEDANS, 7200),
            new CarShopVehicleModel("Washington", VehicleHash.Washington, 0, VEHICLE_CLASS_SEDANS, 6800),
            new CarShopVehicleModel("Stratum", VehicleHash.Stratum, 0, VEHICLE_CLASS_SEDANS, 13000),
            new CarShopVehicleModel("Superd", VehicleHash.Superd, 0, VEHICLE_CLASS_SEDANS, 55000),
            new CarShopVehicleModel("Peyote", VehicleHash.Peyote, 0, VEHICLE_CLASS_SEDANS, 30000),
            new CarShopVehicleModel("Stretch", VehicleHash.Stretch, 0, VEHICLE_CLASS_SEDANS, 95000),

            // Sports
            new CarShopVehicleModel("Alpha", VehicleHash.Alpha, 0, VEHICLE_CLASS_SPORTS, 35600),
            new CarShopVehicleModel("Banshee", VehicleHash.Banshee, 0, VEHICLE_CLASS_SPORTS, 89000),
            new CarShopVehicleModel("Banshee2", VehicleHash.Banshee2, 0, VEHICLE_CLASS_SPORTS, 122000),
            new CarShopVehicleModel("BestiaGTS", VehicleHash.BestiaGTS, 0, VEHICLE_CLASS_SPORTS, 96000),
            new CarShopVehicleModel("Buffalo", VehicleHash.Buffalo, 0, VEHICLE_CLASS_SPORTS, 45000),
            new CarShopVehicleModel("Buffalo2", VehicleHash.Buffalo2, 0, VEHICLE_CLASS_SPORTS, 49000),
            new CarShopVehicleModel("Carbonizzare", VehicleHash.Carbonizzare, 0, VEHICLE_CLASS_SPORTS, 115000),
            new CarShopVehicleModel("Comet2", VehicleHash.Comet2, 0, VEHICLE_CLASS_SPORTS, 96500),
            new CarShopVehicleModel("Comet3", VehicleHash.Comet3, 0, VEHICLE_CLASS_SPORTS, 120000),
            new CarShopVehicleModel("Elegy", VehicleHash.Elegy, 0, VEHICLE_CLASS_SPORTS, 79900),
            new CarShopVehicleModel("Elegy2", VehicleHash.Elegy2, 0, VEHICLE_CLASS_SPORTS, 97000),
            new CarShopVehicleModel("Feltzer2", VehicleHash.Feltzer2, 0, VEHICLE_CLASS_SPORTS, 104000),
            new CarShopVehicleModel("Furoregt", VehicleHash.Furoregt, 0, VEHICLE_CLASS_SPORTS, 93000),
            new CarShopVehicleModel("Fusilade", VehicleHash.Fusilade, 0, VEHICLE_CLASS_SPORTS, 41000),
            new CarShopVehicleModel("Futo", VehicleHash.Futo, 0, VEHICLE_CLASS_SPORTS, 18700),
            new CarShopVehicleModel("Jester", VehicleHash.Jester, 0, VEHICLE_CLASS_SPORTS, 107000),
            new CarShopVehicleModel("Khamelion", VehicleHash.Khamelion, 0, VEHICLE_CLASS_SPORTS, 86000),
            new CarShopVehicleModel("Kuruma", VehicleHash.Kuruma, 0, VEHICLE_CLASS_SPORTS, 65000),
            new CarShopVehicleModel("Lynx", VehicleHash.Lynx, 0, VEHICLE_CLASS_SPORTS, 109000),
            new CarShopVehicleModel("Massacro", VehicleHash.Massacro, 0, VEHICLE_CLASS_SPORTS, 106500),
            new CarShopVehicleModel("Ninef", VehicleHash.Ninef, 0, VEHICLE_CLASS_SPORTS, 112500),
            new CarShopVehicleModel("Ninef2", VehicleHash.Ninef2, 0, VEHICLE_CLASS_SPORTS, 110000),
            new CarShopVehicleModel("Omnis", VehicleHash.Omnis, 0, VEHICLE_CLASS_SPORTS, 89000),
            new CarShopVehicleModel("Penumbra", VehicleHash.Penumbra, 0, VEHICLE_CLASS_SPORTS, 19700),
            new CarShopVehicleModel("RapidGT", VehicleHash.RapidGT, 0, VEHICLE_CLASS_SPORTS, 34600),
            new CarShopVehicleModel("RapidGT2", VehicleHash.RapidGT2, 0, VEHICLE_CLASS_SPORTS, 35100),
            new CarShopVehicleModel("Ruston", VehicleHash.Ruston, 0, VEHICLE_CLASS_SPORTS, 69600),
            new CarShopVehicleModel("Schafter2", VehicleHash.Schafter2, 0, VEHICLE_CLASS_SPORTS, 42200),
            new CarShopVehicleModel("Schafter3", VehicleHash.Schafter3, 0, VEHICLE_CLASS_SPORTS, 53200),
            new CarShopVehicleModel("Schwarzer", VehicleHash.Schwarzer, 0, VEHICLE_CLASS_SPORTS, 48000),
            new CarShopVehicleModel("Seven70", VehicleHash.Seven70, 0, VEHICLE_CLASS_SPORTS, 122000),
            new CarShopVehicleModel("Specter", VehicleHash.Specter, 0, VEHICLE_CLASS_SPORTS, 106000),
            new CarShopVehicleModel("Specter2", VehicleHash.Specter2, 0, VEHICLE_CLASS_SPORTS, 129000),
            new CarShopVehicleModel("Sultan", VehicleHash.Sultan, 0, VEHICLE_CLASS_SPORTS, 45000),
            new CarShopVehicleModel("Surano", VehicleHash.Surano, 0, VEHICLE_CLASS_SPORTS, 63000),
            new CarShopVehicleModel("Tropos", VehicleHash.Tropos, 0, VEHICLE_CLASS_SPORTS, 56000),
            new CarShopVehicleModel("Verlierer2", VehicleHash.Verlierer2, 0, VEHICLE_CLASS_SPORTS, 95000),
            new CarShopVehicleModel("SultanRS", VehicleHash.SultanRS, 0, VEHICLE_CLASS_SPORTS, 110000),
            new CarShopVehicleModel("Coquette3", VehicleHash.Coquette3, 0, VEHICLE_CLASS_SPORTS, 108500),
            new CarShopVehicleModel("Stinger", VehicleHash.Stinger, 0, VEHICLE_CLASS_SPORTS, 50000),
            new CarShopVehicleModel("ZType", VehicleHash.ZType, 0, VEHICLE_CLASS_SPORTS, 55000),
            
            // Classic sports
            new CarShopVehicleModel("Feltzer3", VehicleHash.Feltzer3, 0, VEHICLE_CLASS_SPORTS, 147000),
            new CarShopVehicleModel("Retinue", VehicleHash.Retinue, 0, VEHICLE_CLASS_SPORTS, 38000),
            new CarShopVehicleModel("RapidGT3", VehicleHash.RapidGT3, 0, VEHICLE_CLASS_SPORTS, 57000),
            new CarShopVehicleModel("Infernus2", VehicleHash.Infernus2, 0, VEHICLE_CLASS_SPORTS, 169000),
            new CarShopVehicleModel("Casco", VehicleHash.Casco, 0, VEHICLE_CLASS_SPORTS, 152000),
            new CarShopVehicleModel("Monroe", VehicleHash.Monroe, 0, VEHICLE_CLASS_SPORTS, 145000),
            new CarShopVehicleModel("Stinger", VehicleHash.Stinger, 0, VEHICLE_CLASS_SPORTS, 149000),
            new CarShopVehicleModel("StingerGT", VehicleHash.StingerGT, 0, VEHICLE_CLASS_SPORTS, 151000),
            new CarShopVehicleModel("Mamba", VehicleHash.Mamba, 0, VEHICLE_CLASS_SPORTS, 146000),
            new CarShopVehicleModel("Coquette2", VehicleHash.Coquette2, 0, VEHICLE_CLASS_SPORTS, 139000),
            new CarShopVehicleModel("Coquette", VehicleHash.Coquette, 0, VEHICLE_CLASS_SPORTS, 115000),
            new CarShopVehicleModel("Cheetah2", VehicleHash.Cheetah2, 0, VEHICLE_CLASS_SPORTS, 197000),

		    // Super sports
            new CarShopVehicleModel("Cyclone", VehicleHash.Cyclone, 0, VEHICLE_CLASS_SPORTS, 375000),
            new CarShopVehicleModel("Voltic", VehicleHash.Voltic, 0, VEHICLE_CLASS_SPORTS, 132000),
            new CarShopVehicleModel("GP1", VehicleHash.GP1, 0, VEHICLE_CLASS_SPORTS, 245000),
            new CarShopVehicleModel("Infernus", VehicleHash.Infernus, 0, VEHICLE_CLASS_SPORTS, 135000),
            new CarShopVehicleModel("Bullet", VehicleHash.Bullet, 0, VEHICLE_CLASS_SPORTS, 140000),
            new CarShopVehicleModel("Italigtb2", VehicleHash.ItaliGTB2, 0, VEHICLE_CLASS_SPORTS, 355000),
            new CarShopVehicleModel("Italigtb", VehicleHash.ItaliGTB, 0, VEHICLE_CLASS_SPORTS, 330000),
            new CarShopVehicleModel("Zentorno", VehicleHash.Zentorno, 0, VEHICLE_CLASS_SPORTS, 295000),
            new CarShopVehicleModel("Visione", VehicleHash.Visione, 0, VEHICLE_CLASS_SPORTS, 390000),
            new CarShopVehicleModel("Vagner", VehicleHash.Vagner, 0, VEHICLE_CLASS_SPORTS, 395000),
            new CarShopVehicleModel("Vacca", VehicleHash.Vacca, 0, VEHICLE_CLASS_SPORTS, 198000),
            new CarShopVehicleModel("Turismor", VehicleHash.Turismor, 0, VEHICLE_CLASS_SPORTS, 270000),
            new CarShopVehicleModel("Turismo2", VehicleHash.Turismo2, 0, VEHICLE_CLASS_SPORTS, 260000),
            new CarShopVehicleModel("Tempesta", VehicleHash.Tempesta, 0, VEHICLE_CLASS_SPORTS, 340000),
            new CarShopVehicleModel("T20", VehicleHash.T20, 0, VEHICLE_CLASS_SPORTS, 385000),
            new CarShopVehicleModel("Reaper", VehicleHash.Reaper, 0, VEHICLE_CLASS_SPORTS, 337000),
            new CarShopVehicleModel("Prototipo", VehicleHash.Prototipo, 0, VEHICLE_CLASS_SPORTS, 400000),
            new CarShopVehicleModel("Pfister811", VehicleHash.Pfister811, 0, VEHICLE_CLASS_SPORTS, 335000),
            new CarShopVehicleModel("Penetrator", VehicleHash.Penetrator, 0, VEHICLE_CLASS_SPORTS, 192000),
            new CarShopVehicleModel("Osiris", VehicleHash.Osiris, 0, VEHICLE_CLASS_SPORTS, 341000),
            new CarShopVehicleModel("Nero2", VehicleHash.Nero2, 0, VEHICLE_CLASS_SPORTS, 392000),
            new CarShopVehicleModel("Nero", VehicleHash.Nero, 0, VEHICLE_CLASS_SPORTS, 380000),
            new CarShopVehicleModel("XA21", VehicleHash.XA21, 0, VEHICLE_CLASS_SPORTS, 389000),
            new CarShopVehicleModel("FMJ", VehicleHash.FMJ, 0, VEHICLE_CLASS_SPORTS, 347000),
            new CarShopVehicleModel("EntityXF", VehicleHash.EntityXF, 0, VEHICLE_CLASS_SPORTS, 325000),
            new CarShopVehicleModel("Cheetah", VehicleHash.Cheetah, 0, VEHICLE_CLASS_SPORTS, 260000),
            new CarShopVehicleModel("Adder", VehicleHash.Adder, 0, VEHICLE_CLASS_SPORTS, 310000),
            new CarShopVehicleModel("Sheava", VehicleHash.Sheava, 0, VEHICLE_CLASS_SPORTS, 368000),

            // Motorcycles
            new CarShopVehicleModel("Akuma", VehicleHash.Akuma, 1, VEHICLE_CLASS_MOTORCYCLES, 85000),
            new CarShopVehicleModel("Avarus", VehicleHash.Avarus, 1, VEHICLE_CLASS_MOTORCYCLES, 75000),
            new CarShopVehicleModel("Bagger", VehicleHash.Bagger, 1, VEHICLE_CLASS_MOTORCYCLES, 25000),
            new CarShopVehicleModel("Bati", VehicleHash.Bati, 1, VEHICLE_CLASS_MOTORCYCLES, 67000),
            new CarShopVehicleModel("Bati2", VehicleHash.Bati2, 1, VEHICLE_CLASS_MOTORCYCLES, 70000),
            new CarShopVehicleModel("BF400", VehicleHash.BF400, 1, VEHICLE_CLASS_MOTORCYCLES, 47000),
            new CarShopVehicleModel("Blazer4", VehicleHash.Blazer4, 1, VEHICLE_CLASS_MOTORCYCLES, 27000),
            new CarShopVehicleModel("CarbonRS", VehicleHash.CarbonRS, 1, VEHICLE_CLASS_MOTORCYCLES, 56000),
            new CarShopVehicleModel("Chimera", VehicleHash.Chimera, 1, VEHICLE_CLASS_MOTORCYCLES, 41800),
            new CarShopVehicleModel("Cliffhanger", VehicleHash.Cliffhanger, 1, VEHICLE_CLASS_MOTORCYCLES, 28000),
            new CarShopVehicleModel("Daemon", VehicleHash.Daemon, 1, VEHICLE_CLASS_MOTORCYCLES, 10000),
            new CarShopVehicleModel("Daemon2", VehicleHash.Daemon2, 1, VEHICLE_CLASS_MOTORCYCLES, 12500),
            new CarShopVehicleModel("Defiler", VehicleHash.Defiler, 1, VEHICLE_CLASS_MOTORCYCLES, 61900),
            new CarShopVehicleModel("Double", VehicleHash.Double, 1, VEHICLE_CLASS_MOTORCYCLES, 58000),
            new CarShopVehicleModel("Enduro", VehicleHash.Enduro, 1, VEHICLE_CLASS_MOTORCYCLES, 7000),
            new CarShopVehicleModel("Esskey", VehicleHash.Esskey, 1, VEHICLE_CLASS_MOTORCYCLES, 22400),
            new CarShopVehicleModel("Faggio", VehicleHash.Faggio, 1, VEHICLE_CLASS_MOTORCYCLES, 2100),
            new CarShopVehicleModel("Faggio2", VehicleHash.Faggio2, 1, VEHICLE_CLASS_MOTORCYCLES, 1500),
            new CarShopVehicleModel("Faggio3", VehicleHash.Faggio3, 1, VEHICLE_CLASS_MOTORCYCLES, 2000),
            new CarShopVehicleModel("Fcr", VehicleHash.FCR, 1, VEHICLE_CLASS_MOTORCYCLES, 27000),
            new CarShopVehicleModel("Fcr2", VehicleHash.FCR2, 1, VEHICLE_CLASS_MOTORCYCLES, 32000),
            new CarShopVehicleModel("Gargoyle", VehicleHash.Gargoyle, 1, VEHICLE_CLASS_MOTORCYCLES, 46700),
            new CarShopVehicleModel("Hakuchou", VehicleHash.Hakuchou, 1, VEHICLE_CLASS_MOTORCYCLES, 72000),
            new CarShopVehicleModel("Hakuchou2", VehicleHash.Hakuchou2, 1, VEHICLE_CLASS_MOTORCYCLES, 100000),
            new CarShopVehicleModel("Hexer", VehicleHash.Hexer, 1, VEHICLE_CLASS_MOTORCYCLES, 12500),
            new CarShopVehicleModel("Innovation", VehicleHash.Innovation, 1, VEHICLE_CLASS_MOTORCYCLES, 16000),
            new CarShopVehicleModel("Lectro", VehicleHash.Lectro, 1, VEHICLE_CLASS_MOTORCYCLES, 31800),
            new CarShopVehicleModel("Manchez", VehicleHash.Manchez, 1, VEHICLE_CLASS_MOTORCYCLES, 19000),
            new CarShopVehicleModel("Nemesis", VehicleHash.Nemesis, 1, VEHICLE_CLASS_MOTORCYCLES, 29700),
            new CarShopVehicleModel("Nightblade", VehicleHash.Nightblade, 1, VEHICLE_CLASS_MOTORCYCLES, 35700),
            new CarShopVehicleModel("PCJ", VehicleHash.PCJ, 1, VEHICLE_CLASS_MOTORCYCLES, 33000),
            new CarShopVehicleModel("Ratbike", VehicleHash.RatBike, 1, VEHICLE_CLASS_MOTORCYCLES, 2500),
            new CarShopVehicleModel("Ruffian", VehicleHash.Ruffian, 1, VEHICLE_CLASS_MOTORCYCLES, 32300),
            new CarShopVehicleModel("Sanchez", VehicleHash.Sanchez, 1, VEHICLE_CLASS_MOTORCYCLES, 13100),
            new CarShopVehicleModel("Sanchez2", VehicleHash.Sanchez2, 1, VEHICLE_CLASS_MOTORCYCLES, 23000),
            new CarShopVehicleModel("Sanctus", VehicleHash.Sanctus, 1, VEHICLE_CLASS_MOTORCYCLES, 95000),
            new CarShopVehicleModel("Sovereign", VehicleHash.Sovereign, 1, VEHICLE_CLASS_MOTORCYCLES, 42300),
            new CarShopVehicleModel("Thrust", VehicleHash.Thrust, 1, VEHICLE_CLASS_MOTORCYCLES, 24500),
            new CarShopVehicleModel("Vader", VehicleHash.Vader, 1, VEHICLE_CLASS_MOTORCYCLES, 28100),
            new CarShopVehicleModel("Vindicator", VehicleHash.Vindicator, 1, VEHICLE_CLASS_MOTORCYCLES, 39000),
            new CarShopVehicleModel("Vortex", VehicleHash.Vortex, 1, VEHICLE_CLASS_MOTORCYCLES, 65560),
            new CarShopVehicleModel("Wolfsbane", VehicleHash.Wolfsbane, 1, VEHICLE_CLASS_MOTORCYCLES, 27600),
            new CarShopVehicleModel("Zombiea", VehicleHash.ZombieA, 1, VEHICLE_CLASS_MOTORCYCLES, 24900),
            new CarShopVehicleModel("Zombieb", VehicleHash.ZombieB, 1, VEHICLE_CLASS_MOTORCYCLES, 26100),
            new CarShopVehicleModel("Bmx", VehicleHash.Bmx, 1, VEHICLE_CLASS_MOTORCYCLES, 600),
            new CarShopVehicleModel("Cruiser", VehicleHash.Cruiser, 1, VEHICLE_CLASS_MOTORCYCLES, 350),
            new CarShopVehicleModel("Fixter", VehicleHash.Fixter, 1, VEHICLE_CLASS_MOTORCYCLES, 620),
            new CarShopVehicleModel("Scorcher", VehicleHash.Scorcher, 1, VEHICLE_CLASS_MOTORCYCLES, 500),
            new CarShopVehicleModel("TriBike", VehicleHash.TriBike, 1, VEHICLE_CLASS_MOTORCYCLES, 900),
            new CarShopVehicleModel("Diablous", VehicleHash.Diablous, 1, VEHICLE_CLASS_MOTORCYCLES, 45000),
            new CarShopVehicleModel("Diablous2", VehicleHash.Diablous2, 1, VEHICLE_CLASS_MOTORCYCLES, 40000),

            // Boats
            new CarShopVehicleModel("Dinghy", VehicleHash.Dinghy, 2, VEHICLE_CLASS_BOATS, 25000),
            new CarShopVehicleModel("Dinghy2", VehicleHash.Dinghy2, 2, VEHICLE_CLASS_BOATS, 32000),
            new CarShopVehicleModel("Dinghy3", VehicleHash.Dinghy3, 2, VEHICLE_CLASS_BOATS, 40000),
            new CarShopVehicleModel("Dinghy4", VehicleHash.Dinghy4, 2, VEHICLE_CLASS_BOATS, 55000),
            new CarShopVehicleModel("Jetmax", VehicleHash.Jetmax, 2, VEHICLE_CLASS_BOATS, 80000),
            new CarShopVehicleModel("Marquis", VehicleHash.Marquis, 2, VEHICLE_CLASS_BOATS, 60000),
            new CarShopVehicleModel("Seashark", VehicleHash.Seashark, 2, VEHICLE_CLASS_BOATS, 15000),
            new CarShopVehicleModel("Seashark3", VehicleHash.Seashark3, 2, VEHICLE_CLASS_BOATS, 35000),
            new CarShopVehicleModel("Speeder", VehicleHash.Speeder, 2, VEHICLE_CLASS_BOATS, 72500),
            new CarShopVehicleModel("Speeder2", VehicleHash.Speeder2, 2, VEHICLE_CLASS_BOATS, 90000),
            new CarShopVehicleModel("Suntrap", VehicleHash.Suntrap, 2, VEHICLE_CLASS_BOATS, 30000),
            new CarShopVehicleModel("Squalo", VehicleHash.Squalo, 2, VEHICLE_CLASS_BOATS, 55000),
            new CarShopVehicleModel("Toro", VehicleHash.Toro, 2, VEHICLE_CLASS_BOATS, 90000),
            new CarShopVehicleModel("Toro2", VehicleHash.Toro2, 2, VEHICLE_CLASS_BOATS, 120000),
            new CarShopVehicleModel("Tropic", VehicleHash.Tropic, 2, VEHICLE_CLASS_BOATS, 50000),
            new CarShopVehicleModel("Tropic2", VehicleHash.Tropic2, 2, VEHICLE_CLASS_BOATS, 60000),
            new CarShopVehicleModel("Tug", VehicleHash.Tug, 2, VEHICLE_CLASS_BOATS, 175000)
        };

        // Vehicle's doors
        public const int DRIVER_FRONT_DOOR = 0;
        public const int PASSENGER_FRONT_DOOR = 1;
        public const int DRIVER_REAR_DOOR = 2;
        public const int PASSENGER_REAR_DOOR = 3;
        public const int VEHICLE_HOOD = 4;
        public const int VEHICLE_TRUNK = 5;

        public static List<Vector3> CARSHOP_SPAWNS = new List<Vector3>()
        {
            new Vector3(-47.8021f, -1116.419f, 26.43427f),
            new Vector3(-50.66175f, -1116.753f, 26.4342f),
            new Vector3(-53.51776f, -1116.721f, 26.43449f),
            new Vector3(-56.41209f, -1116.901f, 26.43442f)
        };

        public static List<Vector3> BIKESHOP_SPAWNS = new List<Vector3>()
{
            new Vector3(265.2711f, -1149.21f, 29.29169f),
            new Vector3(262.4801f, -1149.25f, 29.29169f),
            new Vector3(259.3696f, -1149.42f, 29.29169f),
            new Vector3(256.2483f, -1149.431f, 29.29169f),
            new Vector3(250.0457f, -1149.472f, 29.28539f)
        };

        public static List<Vector3> SHIP_SPAWNS = new List<Vector3>()
        {
            new Vector3(-727.1069f, -1327.44f, -0.4730833f),
            new Vector3(-731.7827f, -1334.567f, -0.4733995f),
            new Vector3(-737.4061f, -1340.965f, -0.4733122f),
            new Vector3(-743.5413f, -1347.753f, -0.473477f)
        };

        public static List<GarbageModel> GARBAGE_LIST = new List<GarbageModel>()
        {
            // North
            new GarbageModel(NORTH_ROUTE, 0, new Vector3(-240.8727f, -1346.671f, 30.65419f)),
            new GarbageModel(NORTH_ROUTE, 1, new Vector3(-179.5796f, -1287.857f, 30.87206f)),
            new GarbageModel(NORTH_ROUTE, 2, new Vector3(24.35715f, -366.3141f, 38.88739f)),
            new GarbageModel(NORTH_ROUTE, 3, new Vector3(59.67919f, -226.7723f, 50.62339f)),
            new GarbageModel(NORTH_ROUTE, 4, new Vector3(243.1429f, 169.5262f, 104.5475f)),
            new GarbageModel(NORTH_ROUTE, 5, new Vector3(203.9238f, -159.6877f, 56.32127f)),
            new GarbageModel(NORTH_ROUTE, 6, new Vector3(63.24953f, -398.8811f, 39.49602f)),
            new GarbageModel(NORTH_ROUTE, 7, new Vector3(-13.14317f, -1031.041f, 28.54547f)),
            new GarbageModel(NORTH_ROUTE, 8, new Vector3(-10.67894f, -1085.547f, 26.25084f)),
            new GarbageModel(NORTH_ROUTE, 9, new Vector3(-53.21499f, -1262.405f, 28.6037f)),
            new GarbageModel(NORTH_ROUTE, 10, new Vector3(-151.2484f, -1345.604f, 29.44017f)),

            // South
            new GarbageModel(SOUTH_ROUTE, 0, new Vector3(-223.4353f, -1559.368f, 33.44161f)),
            new GarbageModel(SOUTH_ROUTE, 1, new Vector3(-228.065f, -1633.905f, 33.11632f)),
            new GarbageModel(SOUTH_ROUTE, 2, new Vector3(-14.47537f, -1817.777f, 25.40428f)),
            new GarbageModel(SOUTH_ROUTE, 3, new Vector3(114.0621f, -1943.522f, 20.27394f)),
            new GarbageModel(SOUTH_ROUTE, 4, new Vector3(191.9191f, -1909.875f, 22.67064f)),
            new GarbageModel(SOUTH_ROUTE, 5, new Vector3(283.0735f, -2090.526f, 16.21736f)),
            new GarbageModel(SOUTH_ROUTE, 6, new Vector3(584.6151f, -2816.911f, 5.632026f)),
            new GarbageModel(SOUTH_ROUTE, 7, new Vector3(809.6602f, -2943.637f, 5.48317f)),
            new GarbageModel(SOUTH_ROUTE, 8, new Vector3(851.9996f, -2263.533f, 29.90977f)),
            new GarbageModel(SOUTH_ROUTE, 9, new Vector3(450.8191f, -1970.979f, 22.5295f)),
            new GarbageModel(SOUTH_ROUTE, 10, new Vector3(233.6994f, -1773.683f, 28.25623f)),
            new GarbageModel(SOUTH_ROUTE, 11, new Vector3(-49.93156f, -1486.384f, 31.23168f)),

            // East
            new GarbageModel(EAST_ROUTE, 0, new Vector3(-75.37492f, -1317.174f, 28.6338f)),
            new GarbageModel(EAST_ROUTE, 1, new Vector3(452.5973f, -1070.959f, 28.78741f)),
            new GarbageModel(EAST_ROUTE, 2, new Vector3(808.8442f, -1044.986f, 26.21269f)),
            new GarbageModel(EAST_ROUTE, 3, new Vector3(792.4164f, -913.0083f, 24.84123f)),
            new GarbageModel(EAST_ROUTE, 4, new Vector3(1374.186f, -582.5751f, 73.90349f)),
            new GarbageModel(EAST_ROUTE, 5, new Vector3(1169.929f, -317.8119f, 68.75391f)),
            new GarbageModel(EAST_ROUTE, 6, new Vector3(1079.681f, -792.5755f, 57.84469f)),
            new GarbageModel(EAST_ROUTE, 7, new Vector3(380.5891f, -903.834f, 29.00401f)),
            new GarbageModel(EAST_ROUTE, 8, new Vector3(-27.14754f, -1082.343f, 26.19075f)),
            new GarbageModel(EAST_ROUTE, 9, new Vector3(-167.9472f, -1298.646f, 30.72958f)),
            new GarbageModel(EAST_ROUTE, 10, new Vector3(-240.3486f, -1304.548f, 30.89062f)),

            // West
            new GarbageModel(WEST_ROUTE, 0, new Vector3(-995.6018f, -1116.74f, 1.685367f)),
            new GarbageModel(WEST_ROUTE, 1, new Vector3(-1054.836f, -1021.385f, 1.617427f)),
            new GarbageModel(WEST_ROUTE, 2, new Vector3(-1254.953f, -862.3749f, 11.91052f)),
            new GarbageModel(WEST_ROUTE, 3, new Vector3(-1318.305f, -769.3898f, 19.78054f)),
            new GarbageModel(WEST_ROUTE, 4, new Vector3(-1402.672f, -637.1053f, 28.26154f)),
            new GarbageModel(WEST_ROUTE, 5, new Vector3(-1542.366f, -563.0222f, 33.24453f)),
            new GarbageModel(WEST_ROUTE, 6, new Vector3(-1169.688f, -748.9186f, 18.93798f)),
            new GarbageModel(WEST_ROUTE, 7, new Vector3(-794.9119f, -959.1639f, 14.92298f)),
            new GarbageModel(WEST_ROUTE, 8, new Vector3(-339.9153f, -1316.762f, 30.80578f))
        };

        public static List<Vector3> STOLEN_CAR_CHECKS = new List<Vector3>()
        {
            new Vector3(210.473, -848.802, 29.75367)
        };

        public static List<TunningPriceModel> TUNNING_PRICE_LIST = new List<TunningPriceModel>()
        {
            new TunningPriceModel(VEHICLE_MOD_SPOILER, 250),
            new TunningPriceModel(VEHICLE_MOD_FRONT_BUMPER, 250),
            new TunningPriceModel(VEHICLE_MOD_REAR_BUMPER, 250),
            new TunningPriceModel(VEHICLE_MOD_SIDE_SKIRT, 250),
            new TunningPriceModel(VEHICLE_MOD_EXHAUST, 100),
            new TunningPriceModel(VEHICLE_MOD_FRAME, 500),
            new TunningPriceModel(VEHICLE_MOD_GRILLE, 200),
            new TunningPriceModel(VEHICLE_MOD_HOOD, 300),
            new TunningPriceModel(VEHICLE_MOD_FENDER, 100),
            new TunningPriceModel(VEHICLE_MOD_RIGHT_FENDER, 100),
            new TunningPriceModel(VEHICLE_MOD_ROOF, 400),
            new TunningPriceModel(VEHICLE_MOD_HORN, 100),
            new TunningPriceModel(VEHICLE_MOD_SUSPENSION, 900),
            new TunningPriceModel(VEHICLE_MOD_XENON, 150),
            new TunningPriceModel(VEHICLE_MOD_FRONT_WHEELS, 100),
            new TunningPriceModel(VEHICLE_MOD_BACK_WHEELS, 100),
            new TunningPriceModel(VEHICLE_MOD_PLATE_HOLDERS, 100),
            new TunningPriceModel(VEHICLE_MOD_TRIM_DESIGN, 800),
            new TunningPriceModel(VEHICLE_MOD_ORNAMIENTS, 150),
            new TunningPriceModel(VEHICLE_MOD_STEERING_WHEEL, 100),
            new TunningPriceModel(VEHICLE_MOD_SHIFTER_LEAVERS, 100),
            new TunningPriceModel(VEHICLE_MOD_HYDRAULICS, 1200)
        };

        // Pawn shops
        public static List<Vector3> PAWN_SHOP = new List<Vector3>()
        {
            new Vector3(183.02f, -1319.41f, 29.13f)
        };

        // Weapons crates
        public static List<CrateSpawnModel> CRATE_SPAWN_LIST = new List<CrateSpawnModel>()
        {
            // Island crates
            new CrateSpawnModel(0, new Vector3(-2153.035f, 5202.386f, 13.69618f)),
            new CrateSpawnModel(0, new Vector3(-2165.921f, 5196.308f, 15.8804f)),
            new CrateSpawnModel(0, new Vector3(-2166.648f, 5198.421f, 15.8804f)),
            new CrateSpawnModel(0, new Vector3(-2160.49f, 5205.409f, 15.59176f)),
            new CrateSpawnModel(0, new Vector3(-2170.187f, 5198.082f, 15.91406f)),
            new CrateSpawnModel(0, new Vector3(-2170.373f, 5195.084f, 15.88041f)),
            new CrateSpawnModel(0, new Vector3(-2164.61f, 5206.934f, 15.8803f)),
            new CrateSpawnModel(0, new Vector3(-2163.792f, 5195.11f, 15.37502f)),
            new CrateSpawnModel(0, new Vector3(-2160.487f, 5192.151f, 14.30191f)),
            new CrateSpawnModel(0, new Vector3(-2166.827f, 5207.111f, 15.92977f)),
            new CrateSpawnModel(0, new Vector3(-2168.576f, 5186.567f, 15.04852f)),
            new CrateSpawnModel(0, new Vector3(-2168.642f, 5204.536f, 15.94069f)),
            new CrateSpawnModel(0, new Vector3(-2171.918f, 5205.345f, 16.65958f)),
            new CrateSpawnModel(0, new Vector3(-2172.359f, 5194.064f, 15.8004f)),
            new CrateSpawnModel(0, new Vector3(-2183.144f, 5201.518f, 18.20478f)),
            new CrateSpawnModel(0, new Vector3(-2172.86f, 5196.362f, 15.86153f)),
            new CrateSpawnModel(0, new Vector3(-2187.248f, 5198.493f, 17.98749f)),
            new CrateSpawnModel(0, new Vector3(-2189.329f, 5208.591f, 18.73738f)),
            new CrateSpawnModel(0, new Vector3(-2175.566f, 5197.272f, 15.98747f)),
            new CrateSpawnModel(0, new Vector3(-2189.522f, 5223.072f, 20.30787f)),
            new CrateSpawnModel(0, new Vector3(-2185.964f, 5189.925f, 16.80379f)),
            new CrateSpawnModel(0, new Vector3(-2191.006f, 5229.609f, 20.79736f)),
            new CrateSpawnModel(0, new Vector3(-2196.236f, 5187.129f, 15.81522f)),
            new CrateSpawnModel(0, new Vector3(-2190.801f, 5234.782f, 20.2835f)),
            new CrateSpawnModel(0, new Vector3(-2194.725f, 5178.433f, 14.53511f)),
            new CrateSpawnModel(0, new Vector3(-2185.524f, 5230.828f, 20.47778f)),
            new CrateSpawnModel(0, new Vector3(-2185.441f, 5175.364f, 14.06973f)),
            new CrateSpawnModel(0, new Vector3(-2177.196f, 5228.695f, 18.0752f)),
            new CrateSpawnModel(0, new Vector3(-2175.935f, 5232.989f, 16.57436f)),
            new CrateSpawnModel(0, new Vector3(-2173.745f, 5171.083f, 13.5497f)),
            new CrateSpawnModel(0, new Vector3(-2167.835f, 5238.139f, 15.93195f)),
            new CrateSpawnModel(0, new Vector3(-2165.319f, 5241.51f, 16.14181f)),
            new CrateSpawnModel(0, new Vector3(-2163.495f, 5247.925f, 17.10531f)),
            new CrateSpawnModel(0, new Vector3(-2162.924f, 5251.871f, 17.53788f)),
            new CrateSpawnModel(0, new Vector3(-2169.315f, 5276.271f, 17.32012f)),
            new CrateSpawnModel(0, new Vector3(-2156.115f, 5246.802f, 17.67452f)),
            new CrateSpawnModel(0, new Vector3(-2149.593f, 5240.237f, 15.51855f)),
            new CrateSpawnModel(0, new Vector3(-2146.744f, 5237.425f, 13.9655f)),
            new CrateSpawnModel(0, new Vector3(-2142.169f, 5222.767f, 6.812176f)),
            new CrateSpawnModel(0, new Vector3(-2142.939f, 5209.612f, 9.51135f)),
            new CrateSpawnModel(0, new Vector3(-2156.537f, 5211.339f, 14.68363f)),
            new CrateSpawnModel(0, new Vector3(-2151.335f, 5163.203f, 10.25639f)),
            new CrateSpawnModel(0, new Vector3(-2151.14f, 5152.164f, 9.27355f)),
            new CrateSpawnModel(0, new Vector3(-2159.767f, 5197.274f, 15.10713f)),
            new CrateSpawnModel(0, new Vector3(-2171.098f, 5156.605f, 10.21814f)),
            new CrateSpawnModel(0, new Vector3(-2180.309f, 5144.453f, 2.673023f)),
            new CrateSpawnModel(0, new Vector3(-2194.942f, 5148.009f, 10.29645f)),
            new CrateSpawnModel(0, new Vector3(-2194.885f, 5160.1f, 11.14006f)),
            new CrateSpawnModel(0, new Vector3(-2207.712f, 5161.979f, 13.56075f)),
            new CrateSpawnModel(0, new Vector3(-2209.597f, 5178.93f, 15.06639f)),
            new CrateSpawnModel(0, new Vector3(-2190.155f, 5205.833f, 18.26774f)),
            new CrateSpawnModel(0, new Vector3(-2184.456f, 5200.89f, 18.14797f)),
            new CrateSpawnModel(0, new Vector3(-2208.291f, 5144.133f, 11.18868f)),
            new CrateSpawnModel(0, new Vector3(-2213.94f, 5124.733f, 10.61521f)),
            new CrateSpawnModel(0, new Vector3(-2219.709f, 5108.725f, 10.24075f)),
            new CrateSpawnModel(0, new Vector3(-2208.989f, 5095.347f, 8.770149f)),
            new CrateSpawnModel(0, new Vector3(-2194.331f, 5086.611f, 6.89854f)),
            new CrateSpawnModel(0, new Vector3(-2185.621f, 5108.169f, 7.753129f)),
            new CrateSpawnModel(0, new Vector3(-2192.36f, 5131.622f, 11.40428f)),
            new CrateSpawnModel(0, new Vector3(-2229.719f, 5125.528f, 3.523588f)),
            new CrateSpawnModel(0, new Vector3(-2240.344f, 5116.163f, 2.330123f)),
            new CrateSpawnModel(0, new Vector3(-2236.817f, 5088.094f, 2.087778f)),
            new CrateSpawnModel(0, new Vector3(-2166.787f, 5200.901f, 20.06452)),
            new CrateSpawnModel(0, new Vector3(-2173.201f, 5199.95f, 20.03394f))
        };

        // Weapon and ammunition drop rate
        public static List<WeaponProbabilityModel> WEAPON_CHANCE_LIST = new List<WeaponProbabilityModel>() {
            // Weapons
            new WeaponProbabilityModel(0, "BullpupShotgun", 0, 0, 60),
            new WeaponProbabilityModel(0, "CompactRifle", 0, 61, 75),
            new WeaponProbabilityModel(0, "CarbineRifle", 0, 76, 95),
            new WeaponProbabilityModel(0, "HeavyShotgun", 0, 96, 125),
            new WeaponProbabilityModel(0, "SawnoffShotgun", 0, 126, 215),
            new WeaponProbabilityModel(0, "BullpupRifle", 0, 216, 235),
            new WeaponProbabilityModel(0, "AssaultRifle", 0, 236, 260),
            new WeaponProbabilityModel(0, "APPistol", 0, 261, 320),
            new WeaponProbabilityModel(0, "DoubleBarrelShotgun", 0, 321, 395),
            new WeaponProbabilityModel(0, "MachinePistol", 0, 396, 480),
            new WeaponProbabilityModel(0, "SniperRifle", 0, 481, 495),
            new WeaponProbabilityModel(0, "AssaultSMG", 0, 496, 545),
            new WeaponProbabilityModel(0, "CombatPDW", 0, 546, 580),
            new WeaponProbabilityModel(0, "Revolver", 0, 581, 620),
            new WeaponProbabilityModel(0, "HeavyPistol", 0, 621, 770),
            new WeaponProbabilityModel(0, "PumpShotgun", 0, 771, 850),
            new WeaponProbabilityModel(0, "SpecialCarbine", 0, 851, 865),
            new WeaponProbabilityModel(0, "Pistol50", 0, 866, 1065),
            new WeaponProbabilityModel(0, "AdvancedRifle", 0, 1066, 1085),
            new WeaponProbabilityModel(0, "HeavySniper", 0, 1086, 1100),
            new WeaponProbabilityModel(0, "MicroSMG", 0, 1101, 1150),
            new WeaponProbabilityModel(0, "AssaultShotgun", 0, 1151, 1180),
            new WeaponProbabilityModel(0, "MarksmanRifle", 0, 1181, 1195),
            new WeaponProbabilityModel(0, "SMG", 0, 1196, 1235),

            // Ammunition
            new WeaponProbabilityModel(1, ITEM_HASH_PISTOL_AMMO_CLIP, STACK_PISTOL_CAPACITY, 0, 355),
            new WeaponProbabilityModel(1, ITEM_HASH_MACHINEGUN_AMMO_CLIP, STACK_MACHINEGUN_CAPACITY, 356, 420),
            new WeaponProbabilityModel(1, ITEM_HASH_SHOTGUN_AMMO_CLIP, STACK_SHOTGUN_CAPACITY, 421, 485),
            new WeaponProbabilityModel(1, ITEM_HASH_ASSAULTRIFLE_AMMO_CLIP, STACK_ASSAULTRIFLE_CAPACITY, 486, 495),
            new WeaponProbabilityModel(1, ITEM_HASH_SNIPERRIFLE_AMMO_CLIP, STACK_SNIPERRIFLE_CAPACITY, 496, 500)
        };

        // Highlighted businesses
        public static List<BusinessBlipModel> BUSINESS_BLIP_LIST = new List<BusinessBlipModel>()
        {
            new BusinessBlipModel(3, 93),
            new BusinessBlipModel(117, 93),
            new BusinessBlipModel(259, 71),
            new BusinessBlipModel(276, 68),
            new BusinessBlipModel(280, 136),
            new BusinessBlipModel(282, 121),
            new BusinessBlipModel(283, 446),
            new BusinessBlipModel(287, 75),
            new BusinessBlipModel(288, 73),
            new BusinessBlipModel(297, 52)
        };

        // ATMs
        public static List<Vector3> ATM_LIST = new List<Vector3>()
        {
            new Vector3(-846.6537, -341.509, 37.6685),
            new Vector3(1153.747, -326.7634, 69.2050),
            new Vector3(285.6829, 143.4019, 104.169),
            new Vector3(-847.204, -340.4291, 37.6793),
            new Vector3(-1410.736, -98.9279, 51.397),
            new Vector3(-1410.183, -100.6454, 51.3965),
            new Vector3(-2295.853, 357.9348, 173.6014),
            new Vector3(-2295.069, 356.2556, 173.6014),
            new Vector3(-2294.3, 354.6056, 173.6014),
            new Vector3(-282.7141, 6226.43, 30.4965),
            new Vector3(-386.4596, 6046.411, 30.474),
            new Vector3(24.5933, -945.543, 28.333),
            new Vector3(5.686, -919.9551, 28.4809),
            new Vector3(296.1756, -896.2318, 28.2901),
            new Vector3(296.8775, -894.3196, 28.2615),
            new Vector3(-846.6537, -341.509, 37.6685),
            new Vector3(-847.204, -340.4291, 37.6793),
            new Vector3(-1410.736, -98.9279, 51.397),
            new Vector3(-1410.183, -100.6454, 51.3965),
            new Vector3(-2295.853, 357.9348, 173.6014),
            new Vector3(-2295.069, 356.2556, 173.6014),
            new Vector3(-2294.3, 354.6056, 173.6014),
            new Vector3(-282.7141, 6226.43, 30.4965),
            new Vector3(-386.4596, 6046.411, 30.474),
            new Vector3(24.5933, -945.543, 28.333),
            new Vector3(5.686, -919.9551, 28.4809),
            new Vector3(296.1756, -896.2318, 28.2901),
            new Vector3(296.8775, -894.3196, 28.2615),
            new Vector3(-712.9357, -818.4827, 22.7407),
            new Vector3(-710.0828, -818.4756, 22.7363),
            new Vector3(289.53, -1256.788, 28.4406),
            new Vector3(289.2679, -1282.32, 28.6552),
            new Vector3(-1569.84, -547.0309, 33.9322),
            new Vector3(-1570.765, -547.7035, 33.9322),
            new Vector3(-1305.708, -706.6881, 24.3145),
            new Vector3(-2071.928, -317.2862, 12.3181),
            new Vector3(-821.8936, -1081.555, 10.1366),
            new Vector3(-712.9357, -818.4827, 22.7407),
            new Vector3(-710.0828, -818.4756, 22.7363),
            new Vector3(289.53, -1256.788, 28.4406),
            new Vector3(289.2679, -1282.32, 28.6552),
            new Vector3(-1569.84, -547.0309, 33.9322),
            new Vector3(-1570.765, -547.7035, 33.9322),
            new Vector3(-1305.708, -706.6881, 24.3145),
            new Vector3(-2071.928, -317.2862, 12.3181),
            new Vector3(-821.8936, -1081.555, 10.1366),
            new Vector3(-867.013, -187.9928, 36.8822),
            new Vector3(-867.9745, -186.3419, 36.8822),
            new Vector3(-3043.835, 594.1639, 6.7328),
            new Vector3(-3241.455, 997.9085, 11.5484),
            new Vector3(-204.0193, -861.0091, 29.2713),
            new Vector3(118.6416, -883.5695, 30.1395),
            new Vector3(-256.6386, -715.8898, 32.7883),
            new Vector3(-259.2767, -723.2652, 32.7015),
            new Vector3(-254.5219, -692.8869, 32.5783),
            new Vector3(-867.013, -187.9928, 36.8822),
            new Vector3(-867.9745, -186.3419, 36.8822),
            new Vector3(-3043.835, 594.1639, 6.7328),
            new Vector3(-3241.455, 997.9085, 11.5484),
            new Vector3(-204.0193, -861.0091, 29.2713),
            new Vector3(118.6416, -883.5695, 30.1395),
            new Vector3(-256.6386, -715.8898, 32.7883),
            new Vector3(-259.2767, -723.2652, 32.7015),
            new Vector3(-254.5219, -692.8869, 32.5783),
            new Vector3(89.8134, 2.8803, 67.3521),
            new Vector3(-617.8035, -708.8591, 29.0432),
            new Vector3(-617.8035, -706.8521, 29.0432),
            new Vector3(-614.5187, -705.5981, 30.224),
            new Vector3(-611.8581, -705.5981, 30.224),
            new Vector3(-537.8052, -854.9357, 28.2754),
            new Vector3(-526.7791, -1223.374, 17.4527),
            new Vector3(-1315.416, -834.431, 15.9523),
            new Vector3(-1314.466, -835.6913, 15.9523),
            new Vector3(89.8134, 2.8803, 67.3521),
            new Vector3(-617.8035, -708.8591, 29.0432),
            new Vector3(-617.8035, -706.8521, 29.0432),
            new Vector3(-614.5187, -705.5981, 30.224),
            new Vector3(-611.8581, -705.5981, 30.224),
            new Vector3(-537.8052, -854.9357, 28.2754),
            new Vector3(-526.7791, -1223.374, 17.4527),
            new Vector3(-1315.416, -834.431, 15.9523),
            new Vector3(-1314.466, -835.6913, 15.9523),
            new Vector3(-1205.378, -326.5286, 36.851),
            new Vector3(-1206.142, -325.0316, 36.851),
            new Vector3(147.4731, -1036.218, 28.3678),
            new Vector3(145.8392, -1035.625, 28.3678),
            new Vector3(-1205.378, -326.5286, 36.851),
            new Vector3(-1206.142, -325.0316, 36.851),
            new Vector3(147.4731, -1036.218, 28.3678),
            new Vector3(145.8392, -1035.625, 28.3678),
            new Vector3(-1109.797, -1690.808, 4.375014),
            new Vector3(-721.1284, -415.5296, 34.98175),
            new Vector3(130.1186, -1292.669, 29.26953),
            new Vector3(129.7023, -1291.954, 29.26953),
            new Vector3(129.2096, -1291.14, 29.26953),
            new Vector3(288.8256, -1282.364, 29.64128),
            new Vector3(1077.768, -776.4548, 58.23997),
            new Vector3(527.2687, -160.7156, 57.08937),
            new Vector3(-57.64693, -92.66162, 57.77995),
            new Vector3(527.3583, -160.6381, 57.0933),
            new Vector3(-165.1658, 234.8314, 94.92194),
            new Vector3(-165.1503, 232.7887, 94.92194),
            new Vector3(-1091.462, 2708.637, 18.95291),
            new Vector3(1172.492, 2702.492, 38.17477),
            new Vector3(1171.537, 2702.492, 38.17542),
            new Vector3(1822.637, 3683.131, 34.27678),
            new Vector3(1686.753, 4815.806, 42.00874),
            new Vector3(1701.209, 6426.569, 32.76408),
            new Vector3(-1091.42, 2708.629, 18.95568),
            new Vector3(-660.703, -853.971, 24.484),
            new Vector3(-660.703, -853.971, 24.484),
            new Vector3(-1409.782, -100.41, 52.387),
            new Vector3(-1410.279, -98.649, 52.436),
            new Vector3(-2975.014,380.129,14.99909),
            new Vector3(155.9642,6642.763,31.60284),
            new Vector3(174.1721,6637.943,31.57305),
            new Vector3(-97.33363,6455.411,31.46716),
            new Vector3(-95.49733,6457.243,31.46098),
            new Vector3(-303.2701,-829.7642,32.41727),
            new Vector3(-301.6767,-830.1,32.41727),
            new Vector3(-717.6539,-915.6808,19.21559),
            new Vector3(-1391.023, -590.3637, 30.31957),
            new Vector3(1138.311, -468.941, 66.73091),
            new Vector3(1167.086, -456.1151, 66.79015)
        };

        public static List<Vector3> CAR_LICENSE_CHECKPOINTS = new List<Vector3>()
        {
            new Vector3(-221.63, -1156.04, 21.95),
            new Vector3(-123.01, -1137.775, 24.56),
            new Vector3(-81.99, -1114.58, 24.53),
            new Vector3(-30.89, -976.36, 28.11),
            new Vector3(9.19, -970.07, 28.22),
            new Vector3(77.56, -995.07, 28.22),
            new Vector3(197.29, -1037.55, 28.22),
            new Vector3(381.57, -1054.85, 28.04),
            new Vector3(479.38, -1043.03, 33.35),
            new Vector3(594.48, -1028.04, 35.86),
            new Vector3(760.46, -1008.56, 25.06),
            new Vector3(805.76, -1007.79, 25.02),
            new Vector3(991.11, -1010.97, 40.84),
            new Vector3(811.15, -1084.02, 27.41),
            new Vector3(780.87, -1027.07, 25.05),
            new Vector3(637.33, -1017.92, 35.68),
            new Vector3(415.05, -1045.53, 28.31),
            new Vector3(378.14, -1128.62, 28.18),
            new Vector3(236.98, -1134.07, 28.06),
            new Vector3(181.38, -1132.89, 28.07),
            new Vector3(83.77, -1136.89, 27.93),
            new Vector3(49.46, -1245.83, 28.06),
            new Vector3(69.28, -1341.32, 28.09),
            new Vector3(-73.81, -1364.52, 28.21),
            new Vector3(-210.84, -1413.05, 30.07),
            new Vector3(-268.74, -1404.05, 30.11),
            new Vector3(-265.67, -1191.25, 22.36),
            new Vector3(-246.74, -1148.72, 21.79)
        };

        public static List<Vector3> BIKE_LICENSE_CHECKPOINTS = new List<Vector3>()
        {
            new Vector3(-221.63, -1156.04, 21.95),
            new Vector3(-246.4477, -1132.908, 21.84),
            new Vector3(-510.7749, -1074.368, 21.60),
            new Vector3(-538.9323, -987.1205, 22.16),
            new Vector3(-615.7797, -955.1476, 20.31),
            new Vector3(-748.9092, -953.4031, 16.28),
            new Vector3(-742.6716, -859.597, 21.28),
            new Vector3(-836.6662, -834.0764, 18.21),
            new Vector3(-1061.384, -775.3281, 18.15),
            new Vector3(-1214.395, -605.4772, 25.93),
            new Vector3(-1420.104, -731.9043, 22.30),
            new Vector3(-1400.814, -791.8732, 18.72),
            new Vector3(-1307.51, -893.167, 10.26),
            new Vector3(-1228.521, -1136.944, 6.56),
            new Vector3(-1164.738, -1321.457, 3.91),
            new Vector3(-785.803, -1134.188, 9.42),
            new Vector3(-646.7411, -1292.766, 9.48),
            new Vector3(-542.0643, -1182.95, 17.44),
            new Vector3(-473.185, -1423.335, 28.01),
            new Vector3(-217.8618, -1463.097, 30.09),
            new Vector3(-49.84166, -1598.771, 28.08),
            new Vector3(59.0152, -1523.776, 28.09),
            new Vector3(141.2012, -1417.891, 28.13),
            new Vector3(217.4024, -1146.925, 28.15),
            new Vector3(218.6274, -1067.475, 28.05),
            new Vector3(279.9226, -879.2997, 28.06),
            new Vector3(199.1688, -822.5041, 29.83),
            new Vector3(56.39607, -775.7272, 30.64),
            new Vector3(-108.6394, -1114.953, 24.44),
            new Vector3(-220.5649, -1152.499, 21.8)
        };

        public static List<Vector3> FISHING_POSITION_LIST = new List<Vector3>()
        {
            new Vector3(-1827.806f, -1268.722f, 8.618273f),
            new Vector3(-1832.525f, -1264.608f, 8.618273f),
            new Vector3(-1837.586f, -1260.174f, 8.618273f),
            new Vector3(-1842.251f, -1256.036f, 8.618273f),
            new Vector3(-1847.622f, -1251.621f, 8.618273f),
            new Vector3(-1852.511f, -1248.537f, 8.618273f),
            new Vector3(-1865.566f, -1237.584f, 8.618273f),
            new Vector3(-1827.806f, -1268.722f, 8.618273f)
        };

        public enum AnimationFlags
        {
            Loop = 1 << 0,
            StopOnLastFrame = 1 << 1,
            OnlyAnimateUpperBody = 1 << 4,
            AllowPlayerControl = 1 << 5,
            Cancellable = 1 << 7
        };
    }
}

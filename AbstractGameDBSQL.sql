--USE MASTER;
--CREATE DATABASE AbstractGame;

USE AbstractGame;

--StatIt_[category] signifies its a reference only table and should not be modified, tables with only [category]_[subcategory] or just [name] are dynamic.
--GENERAL STRUCTURE OF CREATE TABLES: First stat wpn then stat item then stat food, then same order dynams

--Shelter
--navigation
--communication
--illumination(vision)
--clothing
--gear

--(Now what has similar attributes????)
--attributes of Axe: weight, effectLevel, durability
--attributes of Foldable saw: weight, effectLevel, durability
--attributes of Nails: weigth, 
--attributes of Rope weight, length(meters int)(durability??), maxLoad
--attributes of Superglue: weight, maxLoad --scrap the maxloads here
--attributes of Tent: weight, color, is WaterProof
--attributes of Radio kit: range, batteryType, duration, weight
--attributes of oil lamp: lightLevel, duration, capacity,  weight, fuel type, 
--attributes of gas stove: heatlevel, duration, fueltype
--attributes of NS-71: lightLevel, intensification, batteryType, duration.
--attributes of Chemstick Red: LightLevel, color, duration
--attributes of

--Clothing first bc thats easiest next to weapons
--clothing attributes: clothName, clothWeight, durability wetProt, tempProt, storage, 
--webbing atributes: webName, WebWeight, durability, prot storage, 
--gear attributes: protection factors of all kind, storage 
--tools

--provision, cutlery and basic hygiene items: how to classify 
--perhaps: mess kit(incl cutlery) int stat_itm_mess, other hygiene items into stat_itm_care

-- Static tables (unchanging data)
--weapons

--TABLE STRUCTURE:
--ID
--NAME
--WEIGHT
--DURABILITY/DURATION
--DURABILITYMOD
--QUALITY
--TYPE
--INTENSITY
--DAMAGE/EFFECTS

--ADD TAG TYPE FOR ITEMS (MAINLY WEAPONS) FOR CREATE INVENTORY METHOD------------------------------------------------------------------------------------------


CREATE TABLE stat_wpn_melee (
    stat_wpn_melee_id INT IDENTITY(1,1) PRIMARY KEY,
    wpnName VARCHAR(255)NOT NULL,
	wpnType VARCHAR(255),
	wpnTag VARCHAR(255),
    wpnRange INT, --if range is over 2 meters its a throwable weapon like spear
    bleedFac FLOAT,
    sharpDmg INT,  
    bluntDmg INT,  
	simpleDmg INT,
    weight FLOAT,
	quality INT NOT NULL
); --spear, throwing knive and throwing axe all go here --done for v1 DB 

CREATE TABLE stat_wpn_gun (
    stat_wpn_gun_id INT IDENTITY(1,1) PRIMARY KEY,
    wpnName VARCHAR(255)NOT NULL,
	wpnType VARCHAR(255),
	wpnTag VARCHAR(255),
    ammoType VARCHAR(50), --LINK TO AMMO TABLE
    magSize INT,
    rpm INT,
    durability INT, -- 1 increment per 100 rounds or so? not sure yet actually  --implement durabilityscale for x shots fired is minus 1 durability, and do for all other tables too
	durabilityMod INT, --if here 1000 then that means per 1000 shots = -1 durability
    compatibility VARCHAR(255),
    weight FLOAT,
	quality INT NOT NULL
); --done for v1 DB

CREATE TABLE stat_wpn_launcher (
    stat_wpn_launcher_id INT IDENTITY(1,1) PRIMARY KEY,
    wpnName VARCHAR(255)NOT NULL,
	wpnType VARCHAR(255),
	wpnTag VARCHAR(255),
    ammoType VARCHAR(50),
    magSize INT,
    reloadTime INT,
    guideSpeed DECIMAL(6,3),
    durability INT,
	durabilityMod INT, --here 1 shotis -1 durability
    weight FLOAT,
	quality INT NOT NULL
); --done for v1 DB

CREATE TABLE stat_wpn_ordnance (
    stat_wpn_ordnance_id INT IDENTITY(1,1) PRIMARY KEY,
    wpnName VARCHAR(255)NOT NULL,
	wpnType VARCHAR(255),
	wpnTag VARCHAR(255),
    wpnRange INT,
    wpnLeRange INT,
	wpnDamage INT,
    duration INT,
    weight FLOAT,
	quality INT NOT NULL
); --done for v1 DB

CREATE TABLE stat_itm_tool ( --Add TYPE like in weapons? (for inventory generation)
	stat_itm_tool_id INT IDENTITY(1,1) PRIMARY KEY,
    itmName VARCHAR(255)NOT NULL,
	weight FLOAT,
	durability INT,
	durabilityMod INT, --variable (100-100 or whatever) uses is -1 durability --check ItemList to see if fixed Modifier is ok
	quality INT,
	effectType VARCHAR(255),
	effectIntensity FLOAT
); --tools like axe shovel etc --done for v1 DB

CREATE TABLE stat_itm_cloth ( 
	stat_itm_cloth_id INT IDENTITY(1,1) PRIMARY KEY,
	clothName VARCHAR(255)NOT NULL,
    clothWeight FLOAT,
    durability INT,
	durabilityMod INT, --3 hours worn is -1 durability (here itm_logic_cloth is responsible for irregular  damages)
    wetProt FLOAT,  
    tempProt FLOAT,  
	meleeProt FLOAT,
	ballisticProt FLOAT,
	fireProt FLOAT,
	chemicalProt FLOAT,
	radiationProt FLOAT,
	storageSlots INT,
	StorageWeight FLOAT, --make sure all load bearing tavles have this
	fatigueCoeff FLOAT,
	quality INT NOT NULL
); --clothing of all kinds, no armor and no webbing 

CREATE TABLE stat_itm_gear (
	stat_itm_gear_id INT IDENTITY(1,1) PRIMARY KEY,
	gearName VARCHAR(255),
	gearWeight FLOAT,
	durability INT,
	durabilityMod INT, --same as cloth, so perhaps the logic script needs to be renamed to "wear" or even "gear"
	wetProt FLOAT,  
    tempProt FLOAT,
	protectionType VARCHAR(255), --this is filterType for masks
	protectionLvl INT,
	fatigueCoeff FLOAT,
	storageSlots INT,
	StorageWeight FLOAT,
	quality INT
); --gear like armor, helmets, etc (gaiters, kneepads too), NO BACKPACKS

CREATE TABLE stat_itm_web (
	stat_itm_gear_id INT IDENTITY(1,1) PRIMARY KEY,
	webWeight FLOAT,
    durability INT,
	durabilityMod INT, --same as gear and cloth? ah hell nah the script needs to be called "wear"
	meleeProt FLOAT,  
	storageSlots INT,
	maxWeight FLOAT,
	fatigueCoeff FLOAT, --in webbing lower than gear for same weight carried
	quality INT NOT NULL
); --chest rig, pouches, backpacks here too despite not classifying as webbing

CREATE TABLE stat_itm_mat (
	stat_itm_mat_id INT IDENTITY(1,1) PRIMARY KEY,
    itmName VARCHAR(255)NOT NULL,
	uses INT, --for tape and wire: 1 use is 1 meter or below.
	weight FLOAT,
	quality INT
); --materials (consumables, but not food or lighting or medical) --AND no durability bc idk why (check this)

CREATE TABLE stat_itm_mess (
	stat_itm_mess_id INT IDENTITY(1,1) PRIMARY KEY,
	itmName VARCHAR(255)NOT NULL,
	itmWeight FLOAT,
	durability INT,
	durabilityMod INT, --same as tools, check itemlist to decide if fixed or variable per item
	quality INT NOT NULL
); --cooking pot, cup, bottles, canteens etc

CREATE TABLE stat_itm_light (
	stat_itm_light_id INT IDENTITY(1,1) PRIMARY KEY,
	itmName VARCHAR(255) nOT NULL,
	itmWeight FLOAT,
	durability INT,
	durabilityMod INT, -- 100 hours is -1 durability
	lightColor VARCHAR(255),
	pattern VARCHAR(255),
	intensity INT,
	Powersource VARCHAR(255),
	capacity FLOAT, --fuel in liter.milliliter and otherwise 0 with batteries
	energyConsumption FLOAT,
	quality INT
); --work on pattern, think the pattern is basically an overlay texture thing reference

CREATE TABLE stat_itm_comm (
	stat_itm_comm_id INT IDENTITY(1,1) PRIMARY KEY,
	itmName VARCHAR(255) NOT NULL,
	itmWeight FLOAT,
	durability INT,
	durabilityMod INT, --100 hours is -1 durability
	range INT,
	emmissonPattern VARCHAR(255),
	wavelenght int, --maybe gotta be float
	frequency INT, --maybe gotta be float
	batteryType VARCHAR(255),
	quality INT
);

CREATE TABLE stat_itm_care (
	stat_itm_care_id INT IDENTITY(1,1) PRIMARY KEY,
	itmName VARCHAR(255) NOT NULL,
	itmWeight FLOAT,
	durability INT, --here for amount of uses, toilet paper is like 20 uses so idk
	durabilityMod INT, --100 uses is -1 durability, for toilet paper: 1 use is -1 durability 
	quality INT

); --stuff like toilet paper cutlery hygiene items

CREATE TABLE stat_itm_med (
	stat_itm_med_id INT IDENTITY(1,1) PRIMARY KEY,
	itmName VARCHAR(255) NOT NULL,
	itmWeight FLOAT,
	durability INT, --watch out, do we even need durability for med items?
	durabilityMod INT, --maybe better not
	healthCoeff FLOAT, --for health REDUCTION
	immunityCoeff FLOAT, --WATCH OUT, Other tables have this or not?2
	staminaCoeff FLOAT,
	maxWeightMod FLOAT,
	bleedRedux FLOAT,
	fireProt FLOAT,
	chemicalProt FLOAT,
	radiationProt FLOAT,
	effectTime FLOAT,
	quality INT
); --perhaps remove durability

CREATE TABLE stat_itm_sust (
	stat_itm_sust_id INT IDENTITY(1,1) PRIMARY KEY,
	itmName VARCHAR(255) NOT NULL,
	itmWeight FLOAT,
	expiryTime INT, --how long the food lasts 1 unit is 1 day
	calories INT,
	healthCoeff FLOAT, -- kept because it effects the same as the hwealthCoeff in stat_itm_med
	quality INT

); --sustenance, all food and drink

CREATE TABLE stat_itm_filter (
	stat_itm_sust_id INT IDENTITY(1,1) PRIMARY KEY,
	itmName VARCHAR(255) NOT NULL,
	itmWeight FLOAT,
	durability INT, 
	durabilityMod INT, --1 day is -1 durability
	fireProt FLOAT,
	chemicalProt FLOAT,
	radiationProt FLOAT,
	bioProt FLOAT,
	fatigueCoeff FLOAT,
	duration INT,
	quality INT
); --merge with stat_type_insert??

CREATE TABLE stat_itm_ammo (
	stat_itm_sust_id INT IDENTITY(1,1) PRIMARY KEY,
	itmName VARCHAR(255) NOT NULL,
	itmWeight FLOAT,
	quality INT
); --check how bullet physics in for example STALKER work, is hitscan or actual simulation --NOT DONE

CREATE TABLE stat_itm_batt (
	stat_itm_sust_id INT IDENTITY(1,1) PRIMARY KEY,
	itmName VARCHAR(255) NOT NULL,
	itmWeight FLOAT,
	--tempRange CUSTOM DATA TYPE?
	capacity FLOAT,
	power FLOAT,
	quality INT
); --batterytypes for now, maybe even add fuel (gotta see later) since different temp ranges for batteries and different capacities

CREATE TABLE stat_itm_fuel (
	stat_itm_fuel_id INT IDENTITY(1,1) PRIMARY KEY,
	itmName VARCHAR(255)NOT NULL,
	itmWeight FLOAT,
	efficiency INT, --higher means less fuel consumption (by altering the energyCOnsumption of the item its used in
	quality INT NOT NULL
); --for gas oil diesel etc

CREATE TABLE stat_type_insert (
	stat_type_insert_id INT IDENTITY(1,1) PRIMARY KEY,
	typeName VARCHAR(255)NOT NULL,
	itypeWeight FLOAT,
	durability INT,
	durabilityMod INT, -- -10 durability for every time shot?? see table cloth for instructions
	meleeProt FLOAT,
	ballisticProt FLOAT,
	fireProt FLOAT, --move all these to specific table for protectiontypes, so here
	chemicalProt FLOAT,
	radiationProt FLOAT,
	quality INT
); --gas mask filters, armor plates, if gear protection is inherent then type here is inherent

CREATE TABLE stat_type_effect (
	stat_type_effect_id INT IDENTITY(1,1) PRIMARY KEY,
	typeName VARCHAR(255)NOT NULL,
); -- for tools mainly

--Special tables:
--transport: heli, plane, glider, parachute, truck, car, motorcycle, bike, skis, sled all later below, how to handle these items? are they entities or items
CREATE TABLE stat_entity_vehicle (
	stat_entity_effect_id INT IDENTITY(1,1) PRIMARY KEY,
	entityName VARCHAR(255)NOT NULL
); --every transport entity, IGNORE
CREATE TABLE stat_entity_creature (
	stat_entity_x_id INT IDENTITY(1,1) PRIMARY KEY,
	entityName VARCHAR(255)NOT NULL
); --for dogs cats and non item using monsters, IGNORE


--NOTE: FOR geiger counter spear whatever, the game needs to know how it works
--will do this by adding itm_logic_device file with logic for geiger counter radio etc etc
--entity_logic_dog for dog entity etc ect


-- Dynamic weapon instances (generated from static templates) for testing db and practising queries

--Here mabye reconsider these dynamic tables
CREATE TABLE wpn_melee (
    wpn_melee_id INT IDENTITY(1,1) PRIMARY KEY,
    stat_wpn_melee_id INT NOT NULL, -- References template
    wpnName VARCHAR(255) NOT NULL, -- Custom name (defaults to template name)
    currentSharpDmg INT, -- Tracks sharp damage over time
    currentBluntDmg INT, -- Tracks blunt damage over time
	currentQuality INT,
    FOREIGN KEY (stat_wpn_melee_id) REFERENCES stat_wpn_melee(wpn_melee_id)
);
CREATE TABLE wpn_gun (
    wpn_gun_id INT IDENTITY(1,1) PRIMARY KEY,
    stat_wpn_gun_id INT NOT NULL, -- References template
	wpnName VARCHAR(255) NOT NULL,
    currentAmmoType VARCHAR(50),
    currentAmmo INT,
    condition INT,					--changed currentDurability to condition
	currentQuality INT,
    FOREIGN KEY (stat_wpn_gun_id) REFERENCES stat_wpn_gun(wpn_gun_id)
);
CREATE TABLE wpn_launcher (
    wpn_launcher_id INT IDENTITY(1,1) PRIMARY KEY,
    stat_wpn_launcher_id INT NOT NULL, -- References template
	wpnName VARCHAR(255) NOT NULL,
    currentAmmoType VARCHAR(50),
    condition INT,						--changed currentDurability to condition	
	currentQuality INT,
    FOREIGN KEY (stat_wpn_launcher_id) REFERENCES stat_wpn_launcher(wpn_launcher_id)
);
CREATE TABLE wpn_ordnance (
    wpn_ordnance_id INT IDENTITY(1,1) PRIMARY KEY,
    stat_wpn_ordnance_id INT NOT NULL, -- References template
	wpnName VARCHAR(255) NOT NULL,
    wpnHealth INT,
    FOREIGN KEY (stat_wpn_ordnance_id) REFERENCES stat_wpn_ordnance(wpn_ordnance_id)
);

-- actor tables (created at gamecreation and after saves)
CREATE TABLE actor (
    actor_id INT IDENTITY(1,1) PRIMARY KEY,
    actorName VARCHAR(255),
    faction VARCHAR(255),
    playstyle VARCHAR(255),
    health INT,
    money INT
); --create separate storage table?
CREATE TABLE storage (
    storage_id INT IDENTITY(1,1) PRIMARY KEY,
    storageName TEXT NOT NULL,   -- Example: "Military Crate"
    locationX FLOAT,      -- World position
    locationY FLOAT,
    locationZ FLOAT
);
CREATE TABLE inventory (
    inv_id INT IDENTITY(1,1) PRIMARY KEY,
    item_id INT NOT NULL,
    owner_id INT NOT NULL,
    ownerType VARCHAR(50),
    equipmentSlot VARCHAR(50),
    FOREIGN KEY (owner_id) REFERENCES actor(actor_id) ON DELETE CASCADE
);
CREATE TABLE droppedItems (
    instance_id INT IDENTITY(1,1) PRIMARY KEY,
    xpos REAL NOT NULL,  
    ypos REAL NOT NULL, 
    zpos REAL NOT NULL, 
    xrot REAL NOT NULL, 
    yrot REAL NOT NULL, 
    zrot REAL NOT NULL,  
    despawnTime INT
);


--checking tables
SELECT * FROM stat_wpn_melee; 
SELECT * FROM stat_wpn_gun;
SELECT * FROM stat_wpn_launcher;
SELECT * FROM stat_wpn_ordnance;
SELECT * FROM stat_itm_tool;
SELECT * FROM stat_itm_mat;
SELECT * FROM stat_itm_care;
SELECT * FROM stat_itm_mess;
SELECT * FROM stat_itm_cloth;
SELECT * FROM stat_itm_gear;
SELECT * FROM stat_itm_web;
SELECT * FROM stat_itm_light;
SELECT * FROM stat_itm_comm;
SELECT * FROM stat_itm_care;
SELECT * FROM stat_itm_ammo;
SELECT * FROM stat_itm_filter; --does this even still need to exist?
SELECT * FROM stat_itm_batt;
SELECT * FROM stat_type_insert;
SELECT * FROM stat_type_effect;



SELECT * FROM wpn_melee, wpn_gun, wpn_launcher, wpn_ordnance;
SELECT * FROM actor, inventory, droppedItems;


--DROP DATABASE AbstractGame;


--FOR TEST SQLite DB v1 CreateInventory test:
CREATE TABLE test_wpn_melee (
    stat_wpn_melee_id INT IDENTITY(1,1) PRIMARY KEY,
    wpnName VARCHAR(255)NOT NULL,
    wpnRange INT, --if range is over 2 meters its a throwable weapon like spear
    bleedFac FLOAT,
    sharpDmg INT,  
    bluntDmg INT,  
	simpleDmg INT,
    weight FLOAT,
	quality INT NOT NULL
); --spear, throwing knive and throwing axe all go here --done for v1 DB 

CREATE TABLE test_wpn_gun (
    stat_wpn_gun_id INT IDENTITY(1,1) PRIMARY KEY, --stat_wpn_gun_id INTEGER PRIMARY KEY AUTOINCREMENT, is the SQLite version.
    wpnName TEXT NOT NULL,
    wpnTag TEXT NOT NULL,
    ammoType TEXT,
    magSize INTEGER,
    rpm INTEGER,
    durability INTEGER,
    durabilityMod INTEGER,
    compatibility TEXT,
    weight REAL,
    quality REAL NOT NULL,
    availability REAL NOT NULL
);
DROP TABLE test_wpn_gun;

CREATE TABLE test_itm_cloth ( 
	stat_itm_cloth_id INT IDENTITY(1,1) PRIMARY KEY,
	clothName VARCHAR(255)NOT NULL,
    clothWeight FLOAT,
    durability INT,
	durabilityMod INT, --3 hours worn is -1 durability (here itm_logic_cloth is responsible for irregular  damages)
    wetProt FLOAT,  
    tempProt FLOAT,  
	meleeProt FLOAT,
	ballisticProt FLOAT,
	fireProt FLOAT,
	chemicalProt FLOAT,
	radiationProt FLOAT,
	storageSlots INT,
	StorageWeight FLOAT, --make sure all load bearing tavles have this
	fatigueCoeff FLOAT,
	quality INT NOT NULL
); --clothing of all kinds, no armor and no webbing 

--TEST INSERTS
INSERT INTO test_wpn_melee (wpnName, wpnRange, bleedFac, sharpDmg, bluntDmg, weight, simpleDmg, quality) VALUES --NOT DONE
('Bat', 180, 0, 0, 0, 0.950, 45, 1), -- Blunt weapon, strong blunt damage, slower
('Machete', 180, 0, 0, 0, 1.120, 55, 2), --same
('Bayonet', 150, 0, 0, 0, 0.450, 35, 3), --not inserted
('USMC Knife', 150, 0, 0, 0, 0.320, 35, 4),  -- Better knife, more durable
('Dagger', 130, 0, 0, 0,  0.185, 60, 5);      -- Slightly weaker, fast for close range

INSERT INTO test_wpn_gun 
(wpnName, wpnTag, ammoType, magSize, rpm, durability, compatibility, weight, quality, availability) --cleaned, shuold work, is done
VALUES 
('Tokarev TT-33', 'light-reliable', '7.62x25mm Tokarev', 8, 600, 75, 'None', 0.85, 3, 9),
('SKS', 'medium-reliable', '7.62x39mm', 10, 400, 100, 'None', 3.8, 3, 9),
('AK-47', 'medium-reliable', '7.62x39mm', 30, 600, 100, 'None', 3.3, 4, 10),
('HK G3', 'heavy-powerful', '7.62x51mm NATO', 20, 600, 100, 'None', 4.1, 6, 5),
('SCAR H', 'heavy-powerful', '7.62x51mm NATO', 20, 650, 95, 'None', 3.9, 8, 2); 

INSERT INTO test_itm_cloth (clothName, clothWeight, durability, durabilityMod, wetProt, meleeProt, ballisticProt, fireProt, chemicalProt, radiationProt, storageSlots, storageWeight, fatigueCoeff, quality) VALUES 
('Jeans', 1),
('Cargopants' 2),
('Army Overall', 3),
('Trenchcoat', 4),
('Gorka 3 Suit', 5);

--select test for the CreateInventory Method.
SELECT * FROM test_wpn_gun WHERE quality > 2 and quality < 8 and wpnTag = 'light-reliable'; --change all params to variables and also change "wpnTag" to a variable string (gearTag, itmTag)

--SQLite version:
SELECT * 
FROM test_wpn_gun 
WHERE quality BETWEEN @MinQuality AND @MaxQuality
  AND availability BETWEEN @MinAvailability AND @MaxAvailability
  AND weight <= @MaxWeight
  AND wpnTag LIKE @TagMatch;


CREATE TABLE test_wpn_gun (
    stat_wpn_gun_id INT IDENTITY(1,1) PRIMARY KEY,
    wpnName VARCHAR(255)NOT NULL,
	wpnTag VARCHAR(255) NOT NULL,
    ammoType VARCHAR(50), --LINK TO AMMO TABLE
    magSize INT,
    rpm INT,
    durability INT, -- 1 increment per 100 rounds or so? not sure yet actually  --implement durabilityscale for x shots fired is minus 1 durability, and do for all other tables too
	durabilityMod INT, --if here 1000 then that means per 1000 shots = -1 durability
    compatibility VARCHAR(255),
    weight FLOAT,
	quality FLOAT NOT NULL,
	availability FLOAT NOT NULL
); --done for v1 DB
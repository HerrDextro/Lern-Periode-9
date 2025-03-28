--USE MASTER;
--CREATE DATABASE AbstractGame;

USE AbstractGame;

--StatIt_[category] signifies its a reference only table and should not be modified, tables with only [category]_[subcategory] or just [name] are dynamic.
--GENERAL STRUCTURE OF CREATE TABLES: First stat wpn then stat item then stat food, then same order dynams


-- Static tables (unchanging data)
--weapons
CREATE TABLE stat_wpn_melee (
    wpn_melee_id INT IDENTITY(1,1) PRIMARY KEY,
    wpnName VARCHAR(255)NOT NULL,
    wpnRange INT,
    bleedFac FLOAT,
    sharpDmg INT,  
    bluntDmg INT,  
	simpleDmg INT,
    weight FLOAT,
	quality INT NOT NULL
); --done for v1 DB

CREATE TABLE stat_wpn_gun (
    wpn_gun_id INT IDENTITY(1,1) PRIMARY KEY,
    wpnName VARCHAR(255)NOT NULL,
    ammoType VARCHAR(50),
    magSize INT,
    rpm INT,
    durability INT, -- 1 increment per 100 rounds or so? not sure yet actually
    compatibility VARCHAR(255),
    weight FLOAT,
	quality INT NOT NULL
); --done for v1 DB

CREATE TABLE stat_wpn_launcher (
    wpn_launcher_id INT IDENTITY(1,1) PRIMARY KEY,
    wpnName VARCHAR(255)NOT NULL,
    ammoType VARCHAR(50),
    magSize INT,
    reloadTime INT,
    guideSpeed DECIMAL(6,3),
    durability INT,
    weight FLOAT,
	quality INT NOT NULL
); --done for v1 DB

CREATE TABLE stat_wpn_ordnance (
    wpn_ordnance_id INT IDENTITY(1,1) PRIMARY KEY,
    wpnName VARCHAR(255)NOT NULL,
    wpnRange INT,
    wpnLeRange INT,
	wpnDamage INT,
    duration INT,
    weight FLOAT,
	quality INT NOT NULL
); --done for v1 DB

--tools
CREATE TABLE stat_itm_tool (
itm_tool_id INT IDENTITY(1,1) PRIMARY KEY,
    itmName VARCHAR(255)NOT NULL,
	weight FLOAT,
	quality INT
);

--materials (consumables, but not food or lighting or medical)
CREATE TABLE stat_itm_mat (
itm_mat_id INT IDENTITY(1,1) PRIMARY KEY,
    itmName VARCHAR(255)NOT NULL,
	uses INT, --for tape and wire: 1 use is 1 meter or below.
	weight FLOAT,
	quality INT
);

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
--attributes of NS-71: lightLevel, intensification, battryType, duration.
--attributes of
--attributes of








-- Dynamic weapon instances (generated from static templates)
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


-- Dynamic tables (changing game state data)
CREATE TABLE actor (
    actor_id INT IDENTITY(1,1) PRIMARY KEY,
    actorName VARCHAR(255),
    faction VARCHAR(255),
    playstyle VARCHAR(255),
    health INT,
    money INT
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
    instance_id INT PRIMARY KEY,
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

SELECT * FROM wpn_melee, wpn_gun, wpn_launcher, wpn_ordnance;
SELECT * FROM actor, inventory, droppedItems;


DROP DATABASE AbstractGame;
ALTER TABLE stat_wpn_melee, stat_wpn_gun, stat_wpn_launcher, stat_wpn_ordnance, wpn_melee, wpn_launcher, wpn_gun, wpn_ordnance
DROP TABLE stat_wpn_melee, stat_wpn_gun, stat_wpn_launcher, stat_wpn_ordnance, wpn_melee, wpn_launcher, wpn_gun, wpn_ordnance;

--USE MASTER;
--CREATE DATABASE AbstractGame;

USE AbstractGame;

--StatIt_[category] signifies its a reference only table and should not be modified, tables with only [category]_[subcategory] or just [name] are dynamic.

BEGIN TRANSACTION;
-- Static weapon tables (unchanging data)
CREATE TABLE stat_wpn_melee (
    wpn_melee_id INT IDENTITY(1,1) PRIMARY KEY,
    wpnName VARCHAR(255),
    wpnRange INT,
    bleedFac FLOAT,
    sharpDmg INT,  -- Initial sharp damage
    bluntDmg INT,  -- Initial blunt damage
    weight FLOAT
);
ALTER TABLE stat_wpn_melee ADD  simpleDmg int;

CREATE TABLE stat_wpn_gun (
    wpn_gun_id INT IDENTITY(1,1) PRIMARY KEY,
    wpnName VARCHAR(255),
    ammoType VARCHAR(50),
    magSize INT,
    rpm INT,
    durability INT,
    compatibility VARCHAR(255),
    weight FLOAT
);

CREATE TABLE stat_wpn_launcher (
    wpn_launcher_id INT IDENTITY(1,1) PRIMARY KEY,
    wpnName VARCHAR(255),
    ammoType VARCHAR(50),
    magSize INT,
    reloadTime INT,
    guideSpeed DECIMAL(6,3),
    durability INT,
    weight FLOAT
);

CREATE TABLE stat_wpn_ordnance (
    wpn_ordnance_id INT IDENTITY(1,1) PRIMARY KEY,
    wpnName VARCHAR(255),
    wpnRange INT,
    wpnLeRange INT,
    duration INT,
    weight FLOAT
);


-- Dynamic weapon instances (generated from static templates)
CREATE TABLE wpn_melee (
    wpn_melee_id INT IDENTITY(1,1) PRIMARY KEY,
    stat_wpn_melee_id INT NOT NULL, -- References template
    wpnName VARCHAR(255) NOT NULL, -- Custom name (defaults to template name)
    currentSharpDmg INT, -- Tracks sharp damage over time
    currentBluntDmg INT, -- Tracks blunt damage over time
    FOREIGN KEY (stat_wpn_melee_id) REFERENCES stat_wpn_melee(wpn_melee_id)
);

CREATE TABLE wpn_gun (
    wpn_gun_id INT IDENTITY(1,1) PRIMARY KEY,
    stat_wpn_gun_id INT NOT NULL, -- References template
	wpnName VARCHAR(255) NOT NULL,
    currentAmmoType VARCHAR(50),
    currentAmmo INT,
    condition INT,					--changed currentDurability to condition
    FOREIGN KEY (stat_wpn_gun_id) REFERENCES stat_wpn_gun(wpn_gun_id)
);

CREATE TABLE wpn_launcher (
    wpn_launcher_id INT IDENTITY(1,1) PRIMARY KEY,
    stat_wpn_launcher_id INT NOT NULL, -- References template
	wpnName VARCHAR(255) NOT NULL,
    currentAmmoType VARCHAR(50),
    condition INT,						--changed currentDurability to condition	
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

COMMIT TRANSACTION;

--checking tables
SELECT * FROM stat_wpn_melee, stat_wpn_gun, stat_wpn_launcher, stat_wpn_ordnance;
SELECT * FROM wpn_melee, wpn_gun, wpn_launcher, wpn_ordnance;
SELECT * FROM actor, inventory, droppedItems;


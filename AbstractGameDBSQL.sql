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
    durability INT, -- 1 increment per 100 rounds or so? not sure yet actually
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
--ALTER TABLE stat_wpn_ordnance ALTER COLUMN wpnLeRange FLOAT NOT NULL;


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
SELECT * FROM stat_wpn_melee; 
SELECT * FROM stat_wpn_gun;
SELECT * FROM stat_wpn_launcher;
SELECT * FROM stat_wpn_ordnance;

SELECT * FROM wpn_melee, wpn_gun, wpn_launcher, wpn_ordnance;
SELECT * FROM actor, inventory, droppedItems;

--inserting data from ItemList
--rules: (strting with melee all the was to ordnance)
--melee range in cm, max value 2,5 meters, ignore all DMG coeffs, just add simpleDmg
--guns: 



-- Insert Melee Weapons (Knife, Dagger, Bat) --DONE
INSERT INTO stat_wpn_melee (wpnName, wpnRange, bleedFac, sharpDmg, bluntDmg, weight, simpleDmg) VALUES
('Hunter''s knife', 130, 0, 0, 0, 0.410, 25),  -- Common melee knife, decent damage
('USMC Knife', 150, 0, 0, 0, 0.320, 35),  -- Better knife, more durable
('Dagger', 130, 0, 0, 0,  0.185, 60),        -- Slightly weaker, fast for close range
('Bat', 200, 0, 0, 0, 0.950, 45); -- Blunt weapon, strong blunt damage, slower
--DELETE FROM stat_wpn_melee WHERE sharpDmg =  0;

-- Insert Guns (Handguns, SMGs, Assault Rifles) --DONE
INSERT INTO stat_wpn_gun (wpnName, ammoType, magSize, rpm, durability, compatibility, weight) VALUES
('Tokarev TT-33', '7.62x25mm Tokarev', 8, 600, 75, 'None', 0.85),  -- Old gun, lower durability, decent rate of fire
('S&W .32 revolver', '.32 ACP', 6, 500, 90, 'None', 0.75), -- Revolver, low capacity but reliable
('Glock 17', '9x19mm Parabellum', 17, 800, 95, 'None', 0.85),         -- Standard, reliable, decent durability
('MP5K', '9x19mm Parabellum', 30, 900, 80, 'None', 2.5),              -- SMG, fast rate of fire
('PPSh-41', '7.62x25mm Tokarev', 71, 1000, 70, 'None', 5.5),       -- Older but high fire rate, large magazine
('AK-47', '7.62x39mm', 30, 600, 100, 'None', 3.3),         -- Iconic rifle, high durability
('VSS Vintorez', '9x39mm', 10, 600, 85, 'None', 3.2),    -- Quiet sniper, good for stealth
('SCAR H', '7.62x51mm NATO', 20, 650, 95, 'None', 3.9),         -- Modern, very high durability
('SIG 553', '5.56x45mm NATO', 30, 700, 90, 'None', 3.4);        -- Modern rifle for specops (expensive too)

-- Insert Sniper Rifles --DONE
INSERT INTO stat_wpn_gun (wpnName, ammoType, magSize, rpm, durability, compatibility, weight) VALUES
('Mosin Nagant', '7.62x54R', 5, 40, 80, 'None', 4.0),     -- Classic, solid long-range
('Dragunov', '7.62x54R', 10, 30, 90, 'None', 4.5),        -- Semi-auto, good range
('L96', '7.62x51 NATO', 5, 40, 95, 'None', 6.0),              -- High-end sniper
('Barrett M82', '12.7x99mm', 10, 30, 100, 'None', 13.0);   -- Heavy sniper, high damage and durability

-- Insert Shotguns --DONE
INSERT INTO stat_wpn_gun (wpnName, ammoType, magSize, rpm, durability, compatibility, weight) VALUES
('TOZ-106', '12ga', 3, 40, 80, 'None', 3.0),      -- Low end shotgun
('Mossberg 500', '12ga', 5, 60, 85, 'None', 3.5),  -- Reliable, good durability
('Saiga 12', '12ga', 10, 80, 90, 'None', 4.0);     -- High rate of fire, good for close combat

-- Insert Launchers --DONE
INSERT INTO stat_wpn_launcher (wpnName, ammoType, magSize, reloadTime, guideSpeed, durability, weight) VALUES
('RPG-7', 'HEAT', 1, 5, 0, 90, 10.0),      -- Anti-tank, slow reload but high durability
('AT-4',  'HEAT', 1, 0, 0, 1, 6.7),          --affordable effective against Armored vehicles, used by anyone with a supporting group
('Javelin', 'Thermal', 1, 8, 0.5, 95, 15.0), -- Advanced guided missile, good durability
('M302 grenade launcher', 'Frag', 6, 2, 0, 70, 4.0); -- Grenade launcher, for spec ops ppl

-- Insert Ordnance --DONE
INSERT INTO stat_wpn_ordnance (wpnName, wpnRange, wpnLeRange, duration, weight) VALUES --duration is int in seconds, Range is float (1 metr . 10 cm)
('RDG-5', 15, 3.5, 3, 0.3),    -- Grenade, short range, lightweight
('M67', 15, 4.0, 4, 0.4),      -- Standard grenade, medium range
('Molotov', 10, 1.5, 10, 0.5),  -- Fire-based ordnance, medium range
('C4 explosive', 0, 20, 30, 1.0), -- Powerful explosive, heavy weight
('PFM-1 Mine', 0, 2.0, 300, 0.075 ), --anti personnel wounding mine
('VS-50', 0, 3, 0,  0.185);

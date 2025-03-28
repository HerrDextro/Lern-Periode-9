USE AbstractGame;

--inserting data from ItemList
--rules: (starting with melee all the way to ordnance, then normal items)
--melee range in cm, max value 2,5 meters, ignore all DMG coeffs, just add simpleDmg
--guns: 

--Ammo types(small to large, shotgun):
--: .22 LR
--: 5.7x28mm
--: .32 ACP
--: 7.62x25mm Tokarev
--: 9x18mm Makarov
--: 9x19mm Parabellum
--: .45 ACP
--: .50 AE
--: 9x39mm
--: 5.56x45mm NATO
--: 6.5 Creedmoor
--: 7.63x39mm
--: 8.6 Blackout
--: .40 Lobaev Whisper
--: 7.62x51mm NATO
--: 7.5x55mm Swiss
--: 7.62x54R 
--: 7.92x57mm Mauser
--: .338 Lapua 
--: .338 Norma
--: .50 BMG
--: 12.7x108mm
--: 12 Gauge
--: 4 Gauge
--: 40mm 
--: PG-7V
--: 


-- Insert Melee Weapons (Knife, Dagger, Bat) 
INSERT INTO stat_wpn_melee (wpnName, wpnRange, bleedFac, sharpDmg, bluntDmg, weight, simpleDmg, quality) VALUES
('Bayonet', 150, 0, 0, 0, 0.450, 35, 3), --not inserted
('Kukri', 160, 0, 0, 0, 0.670, 50, 5),
('Machete', 180, 0, 0, 0, 1.120, 55, 2), --same
('Club', 170, 0, 0, 0, 1.650, 80, 2), --;until here new insert
('Hunter''s knife', 130, 0, 0, 0, 0.410, 25, 3),  -- Common melee knife, decent damage
('USMC Knife', 150, 0, 0, 0, 0.320, 35, 3),  -- Better knife, more durable
('Dagger', 130, 0, 0, 0,  0.185, 60, 5),        -- Slightly weaker, fast for close range
('Bat', 180, 0, 0, 0, 0.950, 45, 1); -- Blunt weapon, strong blunt damage, slower


-- Insert Guns (Handguns, SMGs, Assault Rifles) 
INSERT INTO stat_wpn_gun (wpnName, ammoType, magSize, rpm, durability, compatibility, weight, quality) VALUES
('Beretta M9', '9x19mm Parabellum', 15, 1100, 100, 'none', 0.950, 3),
('SIG P226', '9x19mm Parabellum', 15, 1100, 100, 'none', 0.964, 4),
('M1911', '.45 ACP', 7, 900, 100, 'none', 1.100, 3),
('MP5', '9x19mm Parabellum', 30, 800, 100, 'none', 2.540, 4),
('Uzi', '9x19mm Parabellum', 32, 600, 100, 'none', 3.500, 4),
('MAC-10', '.45 ACP', 30, 1100, 100, 'none', 2.840, 4),
('P90', '5.7x28mm', 50, 900, 100, 'none', 2.540, 5),
('AK-74', '5.56x45mm NATO', 30, 600, 100, 'none', 3.300, 4),
('M4A1', '5.56x45mm NATO', 30, 700, 100, 'none', 3.100, 4),
('FN FAL', '7.62x51mm NATO', 20, 650, 100, 'none', 4.450, 3),
('HK G3', '7.62x51mm NATO', 20, 600, 100, 'none', 4.100, 4),
('SKS', '7.62x39mm', 10, 400, 100, 'none', 3.800, 2),
('VSS-M', '9x39mm', 10, 600, 100, 'none', 2.600, 5),
('AS VAL', '9x39mm', 20, 900, 100, 'none', 2.500, 4),
('SCAR-L', '5.56x45mm NATO', 30, 625, 100, 'none', 3.500, 5),
('Steyr AUG', '5.56x45mm NATO', 30, 680, 100, 'none', 3.600, 3),
('Stgw 57', '7.5x55mm Swiss', 24, 600, 100, 'none', 6.100, 4),
('Stgw 90', '5.56x45mm NATO', 30, 600, 100, 'none', 4.100, 3), 
('DhsK', '12.7x108mm', 50, 600, 100, 'none', 34.000, 5),		--MG's from here
('MG34', '7.92x57mm Mauser', 50, 900, 100, 'none', 12.100, 5),
('MG42', '7.92x57mm Mauser', 50, 1300, 100, 'none', 11.600, 4),
('MG51', '7.5x55mm Swiss', 50, 1000, 100, 'none', 13.000, 5),
('DP27', '7.62x54R', 47, 550, 100, 'none', 9.200, 3),
('DP28', '7.62x54R', 47, 550, 100, 'none', 9.100, 3),
('ZB vz. 26', '7.92x57mm Mauser', 30, 500, 100, 'none', 8.900, 4),
('UK vz. 59', '7.62x54R', 50, 600, 100, 'none', 9.300, 4),
('AA-52', '7.62x51mm NATO', 100, 900, 100, 'none', 10.500, 4),
('RPD', '7.62x39mm', 100, 750, 100, 'none', 7.400, 4),
('RPK', '7.62x39mm', 75, 600, 100, 'none', 5.000, 4),
('M249', '5.56x45mm NATO', 100, 850, 100, 'none', 7.500, 5),
('PKP', '7.62x54R', 100, 800, 100, 'none', 9.500, 5),
('PK', '7.62x54R', 100, 700, 100, 'none', 9.000, 4),
('Tavor X95', '5.56x45mm NATO', 30, 750, 100, 'none', 3.400, 5),
('Tokarev TT-33', '7.62x25mm Tokarev', 8, 600, 75, 'None', 0.85, 2),  -- Old gun, lower durability, decent rate of fire
('S&W .32 revolver', '.32 ACP', 6, 500, 90, 'None', 0.75, 3), -- Revolver, low capacity but reliable
('Glock 17', '9x19mm Parabellum', 17, 800, 95, 'None', 0.85, 4),         -- Standard, reliable, decent durability
('MP5K', '9x19mm Parabellum', 30, 900, 80, 'None', 2.5, 5),              -- SMG, fast rate of fire
('PPSh-41', '7.62x25mm Tokarev', 71, 1000, 70, 'None', 5.5, 2),       -- Older but high fire rate, large magazine
('AK-47', '7.62x39mm', 30, 600, 100, 'None', 3.3, 2),         -- Iconic rifle, high durability
('VSS Vintorez', '9x39mm', 10, 600, 85, 'None', 3.2, 4),    -- Quiet sniper, good for stealth
('SCAR H', '7.62x51mm NATO', 20, 650, 95, 'None', 3.9, 5),         -- Modern, very high durability
('SIG 553', '5.56x45mm NATO', 30, 700, 90, 'None', 3.4, 5);        -- Modern rifle for specops (expensive too)

-- Insert Sniper Rifles 
INSERT INTO stat_wpn_gun (wpnName, ammoType, magSize, rpm, durability, compatibility, weight, quality) VALUES
('Remington 700', '7.62x51mm NATO', 5, 40, 100, 'none', 4.080, 4),
('Dragunov SVD', '7.62x54R', 10, 180, 100, 'none', 4.310, 4),
('M24', '7.62x51mm NATO', 5, 40, 100, 'none', 5.400, 4),
('DVL-10 Urbana', '.338 Lapua', 5, 30, 100, 'none', 5.200, 5),
('DVL-10 Saboteur', '.40 Lobaev Whisper', 5, 30, 100, 'none', 4.900, 5),
('Kar98k', '7.92x57mm Mauser', 5, 30, 100, 'none', 3.900, 3),
('Karabiner 11', '7.5x55mm Swiss', 6, 35, 100, 'none', 4.200, 2),
('Karabiner 31', '7.5x55mm Swiss', 6, 35, 100, 'none', 4.100, 3),
('Mosin Nagant', '7.62x54R', 5, 40, 80, 'None', 4.0, 2),     -- Classic, solid long-range
('L96', '7.62x51 NATO', 5, 40, 95, 'None', 6.0, 4),              -- High-end sniper --MISSING MM AFTER 7.62x51 !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
('Barrett M82', '12.7x99mm', 10, 30, 100, 'None', 13.0, 5);   -- Heavy sniper, high damage and durability

-- Insert Shotguns 
INSERT INTO stat_wpn_gun (wpnName, ammoType, magSize, rpm, durability, compatibility, weight, quality) VALUES
('DS-12', 0, 0, 0, 0, 'none', 0.00, 5), --double barrel double deef tube
('Striker-12', 0, 0, 0, 0, 'none', 0.00, 3),
('Remington 870', 0, 0, 0, 0, 'none', 0.00, 3),
('KS-23', 0, 0, 0, 0, 'none', 0.00, 5),
('Benelli M4', 0, 0, 0, 0, 'none', 0.00, 4), --new inserts until here
('TOZ-106', '12ga', 3, 40, 80, 'None', 3.0, 3),      -- Low end shotgun
('Mossberg 500', '12ga', 5, 60, 85, 'None', 3.5, 3),  -- Reliable, good durability
('Saiga 12', '12ga', 10, 80, 90, 'None', 4.0, 5);     -- High rate of fire, good for close combat

-- Insert Launchers 
INSERT INTO stat_wpn_launcher (wpnName, ammoType, magSize, reloadTime, guideSpeed, durability, weight, quality) VALUES
('M320 Grenade Launcher', 'Frag', 1, 4, 0, 1000, 1.50, 4), --new inserts
('RPG-7', 'HEAT', 1, 5, 0, 900, 10.0, 4),      -- Anti-tank, slow reload but high durability
('AT-4',  'HEAT', 1, 0, 0, 1000, 6.7, 4),          --affordable effective against Armored vehicles, used by anyone with a supporting group
('Javelin', 'HEAT', 1, 8, 0.5, 95, 15.0, 5), -- Advanced guided missile, good durability
('M302 Grenade Launcher', 'Frag', 6, 2, 0, 70, 4.0, 5); -- Grenade launcher, for spec ops ppl

-- Insert Ordnance 
INSERT INTO stat_wpn_ordnance (wpnName, wpnRange, wpnLeRange, wpnDamage, duration, weight, quality) VALUES --duration is int in seconds, Range is float (1 metr . 10 cm)
('RDG-5', 15, 3.5, 3, 0.3, 3),    -- Grenade, short range, lightweight
('M67', 15, 4.0, 4, 0.4, 4),      -- Standard grenade, medium range
('Molotov', 10, 1.5, 10, 0.5, 2),  -- Fire-based ordnance, medium range
('C4 explosive', 0, 20, 30, 1.0, 5), -- Powerful explosive, heavy weight
('PFM-1 Mine', 0, 2.0, 300, 0.075, 4), --anti personnel wounding mine
('VS-50', 0, 3, 0,  0.185, 5),  --used alot by elites
('Shaped Charge', 0, 1, 20, 0.220, 5); --breach only


--alters & inserts
ALTER TABLE stat_wpn_ordnance ALTER COLUMN wpnLeRange FLOAT NOT NULL;
ALTER TABLE x ADD quality INT NOT NULL; --quality: 1(bad, available), 2(cheap, minimal), 3(decent, affordable), 4(good, expensive), 5(rare, powerful);

ALTER TABLE stat_wpn_ordnance ADD quality INT; -- make not null after altering (ofc not possible to do instantly


UPDATE stat_wpn_ordnance --first time using CASE in SQL 
SET quality = CASE 
    WHEN wpnName = 'RDG-5' THEN 3
    WHEN wpnName = 'M67' THEN 4
END;


--DELETE FROM stat_wpn_melee WHERE weight IS NULL;
SELECT * FROM stat_wpn_melee; 

--checking tables
SELECT * FROM stat_wpn_melee; 
SELECT * FROM stat_wpn_gun;
SELECT * FROM stat_wpn_launcher;
SELECT * FROM stat_wpn_ordnance;

SELECT * FROM wpn_melee, wpn_gun, wpn_launcher, wpn_ordnance;
SELECT * FROM actor, inventory, droppedItems;
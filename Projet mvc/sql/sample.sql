INSERT INTO users (username, email, password_hash, salt, role, creation_date)
VALUES (
    'testuser',                     -- username
    'test@example.com',             -- email
    'fake_hash_placeholder',        -- password_hash (Replace with actual hash in real app)
    'fake_salt_placeholder',        -- salt (Replace if needed)
    'user',                         -- role
    CURRENT_TIMESTAMP               -- creation_date
);

-- === 2. INSERT SAMPLE LISTINGS (associated with user_id = 1) ===

-- Listing 1
INSERT INTO listings (title, description, price, is_available, user_id, creation_date)
VALUES (
    'Console Super Nintendo SNES en Boîte',
    'Super Nintendo PAL en boîte avec 1 manette, câbles et jeu Super Mario World. Testée et fonctionnelle. Boîte en état moyen, voir photos.',
    125.00, true, 1, CURRENT_TIMESTAMP - INTERVAL '2 days'
);

-- Listing 2
INSERT INTO listings (title, description, price, is_available, user_id, creation_date)
VALUES (
    'Lot Jeux GameBoy Color',
    'Lot de 5 jeux pour GameBoy Color : Pokemon Jaune, Zelda Oracle of Ages, Wario Land 3, Tetris DX, Mario Tennis. Cartouches seules, sauvegardes OK.',
    75.50, true, 1, CURRENT_TIMESTAMP - INTERVAL '1 day'
);

-- Listing 3
INSERT INTO listings (title, description, price, is_available, user_id, creation_date)
VALUES (
    'Sega Mega Drive II + Sonic 2',
    'Console Sega Mega Drive 2 avec une manette officielle, alimentation, câble vidéo. Inclut le jeu Sonic the Hedgehog 2 en cartouche. Parfait état de marche.',
    60.00, true, 1, CURRENT_TIMESTAMP - INTERVAL '1 hour'
);

-- Listing 4 (Indisponible)
INSERT INTO listings (title, description, price, is_available, user_id, creation_date)
VALUES (
    'Manette Nintendo 64 Grise Officielle',
    'Manette N64 grise, officielle Nintendo. Stick analogique un peu lâche mais fonctionnel. Bon état cosmétique.',
    15.00, false, 1, CURRENT_TIMESTAMP - INTERVAL '5 days'
);

-- Listing 5
INSERT INTO listings (title, description, price, is_available, user_id, creation_date)
VALUES (
    'PlayStation 1 (PSX) Modèle SCPH-7502',
    'Console Sony PlayStation 1 avec puce (lit les backups/imports). Fournie avec une manette DualShock, carte mémoire et câbles. Testée OK.',
    55.00, true, 1, CURRENT_TIMESTAMP - INTERVAL '6 hours'
);

-- Listing 6
INSERT INTO listings (title, description, price, is_available, user_id, creation_date)
VALUES (
    'Jeu Zelda: A Link to the Past SNES (Loose)',
    'Cartouche seule du jeu The Legend of Zelda: A Link to the Past pour Super Nintendo. Version PAL FRA. Étiquette un peu usée.',
    35.00, true, 1, CURRENT_TIMESTAMP - INTERVAL '3 days'
);

-- Listing 7
INSERT INTO listings (title, description, price, is_available, user_id, creation_date)
VALUES (
    'Atari 2600 Woody (4 interrupteurs)',
    'Console Atari 2600 version "Woody" à 4 interrupteurs. Avec 2 joysticks classiques et alimentation. Pas de jeux inclus. Fonctionne parfaitement.',
    80.00, true, 1, CURRENT_TIMESTAMP - INTERVAL '10 hours'
);

-- Listing 8
INSERT INTO listings (title, description, price, is_available, user_id, creation_date)
VALUES (
    'Game Gear Noire + Colonne de Tetris',
    'Console portable Sega Game Gear noire. Son et image OK. Quelques rayures d''usage. Vendue avec le jeu Columns et Tetris (2 en 1). Sans adaptateur secteur.',
    70.00, true, 1, CURRENT_TIMESTAMP - INTERVAL '1 day 5 hours'
);

-- Listing 9 (Indisponible)
INSERT INTO listings (title, description, price, is_available, user_id, creation_date)
VALUES (
    'Jeu Final Fantasy VII PS1 (Platinum)',
    'Jeu Final Fantasy VII sur PlayStation 1. Version Platinum française. Boitier et CD en bon état, manque la notice.',
    25.00, false, 1, CURRENT_TIMESTAMP - INTERVAL '7 days'
);

-- Listing 10
INSERT INTO listings (title, description, price, is_available, user_id, creation_date)
VALUES (
    'NES Action Set en Boîte (Nintendo)',
    'Console Nintendo Entertainment System (NES) version Action Set (avec pistolet Zapper et Super Mario Bros./Duck Hunt). Complète en boîte. État collection.',
    180.00, true, 1, CURRENT_TIMESTAMP - INTERVAL '4 hours'
);

-- Listing 11
INSERT INTO listings (title, description, price, is_available, user_id, creation_date)
VALUES (
    'Neo Geo Pocket Color Bleu Translucide',
    'Console portable SNK Neo Geo Pocket Color, modèle bleu translucide. Très bon état, écran sans rayures. Sans jeux ni boîte.',
    110.00, true, 1, CURRENT_TIMESTAMP - INTERVAL '15 hours'
);

-- Listing 12
INSERT INTO listings (title, description, price, is_available, user_id, creation_date)
VALUES (
    'Lot Accessoires Super Nintendo',
    'Lot comprenant : Adaptateur Super Game Boy, Souris Mario Paint + Tapis, Manette non officielle. Le tout fonctionnel.',
    45.00, true, 1, CURRENT_TIMESTAMP - INTERVAL '2 days 2 hours'
);
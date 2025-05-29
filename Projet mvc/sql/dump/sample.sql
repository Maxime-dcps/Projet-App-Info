INSERT INTO users (username, email, password_hash, salt, role, creation_date)
VALUES (
    'testuser',                     -- username
    'test@example.com',             -- email
    'fake_hash_placeholder',        -- password_hash (Replace with actual hash in real app)
    'fake_salt_placeholder',        -- salt (Replace if needed)
    'user',                         -- role
    CURRENT_TIMESTAMP               -- creation_date
),

('retrofan92', 'retrofan92@example.com', 'hash_placeholder_2', 'salt_placeholder_2', 'User', CURRENT_TIMESTAMP - INTERVAL '3 days'),

('vintagegamer', 'vintagegamer@example.com', 'hash_placeholder_3', 'salt_placeholder_3', 'User', CURRENT_TIMESTAMP - INTERVAL '5 days'),

('pixelhunter', 'pixelhunter@example.com', 'hash_placeholder_4', 'salt_placeholder_4', 'User', CURRENT_TIMESTAMP - INTERVAL '1 day'),

('NicolCage', 'Nicolas.Cage@example.com', 'hash_placeholder_5', 'salt_placeholder_5', 'User', CURRENT_TIMESTAMP - INTERVAL '10 days');

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

INSERT INTO images (file_path, image_order, alt_text, listing_id, upload_date) VALUES
('/images/uploads/md2_console.jpg', 0, 'Console Sega Mega Drive II vue de dessus', 3, '2025-05-01 15:40:50.812459'),
('/images/uploads/md2_pad.jpg', 1, 'Manette officielle Sega Mega Drive 3 boutons', 3, '2025-05-01 15:40:50.812459'),
('/images/uploads/md2_sonic2.jpg', 2, 'Cartouche du jeu Sonic the Hedgehog 2 pour Mega Drive', 3, '2025-05-01 15:40:50.812459'),
('/images/uploads/59b538ed-4286-4ace-9644-7ade00b90abc.jpg', 0, 'fad793e140ac174dea9a90e545c837c7f5bb2f2d', 10, '2025-05-29 11:10:02.953754'),
('/images/uploads/938f5982-afdf-402f-8f3c-38b3c998f76f.jpg', 1, 'nes-console-2-manettes-zapper-e164337', 10, '2025-05-29 11:10:02.956796'),
('/images/uploads/2a4bda04-4e87-4336-b88c-ea90eb124fab.jpg', 2, 's-l400', 10, '2025-05-29 11:10:02.961002'),
('/images/uploads/b13ea412-4e36-430b-a81f-3b2769e17b3c.jpg', 0, 's-l1600', 5, '2025-05-29 11:10:59.150686'),
('/images/uploads/20db1661-892e-4c06-a4e8-c1d75d733c46.jpg', 0, 'console-atari-2600-woody-en-boite', 7, '2025-05-29 11:12:27.274283'),
('/images/uploads/0a5b768f-e275-457a-81a0-24e8cc7e5c1d.jpg', 1, 's-l400 (1)', 7, '2025-05-29 11:12:27.277552'),
('/images/uploads/2d356c0a-8f8c-49f8-a5bf-d5381646d6a1.jpg', 2, 's-l400 (2)', 7, '2025-05-29 11:12:27.280183'),
('/images/uploads/97fd16ad-e128-4b90-99d8-e3842ab1c8ed.jpg', 1, 's-l1200 (1)', 11, '2025-05-29 11:13:12.878281'),
('/images/uploads/a9d73019-8754-4482-9b6f-bbb7361d69cc.jpg', 2, 's-l1200', 11, '2025-05-29 11:13:12.884856'),
('/images/uploads/b8eb8f11-32c1-48aa-9b7b-fe9456aca2a4.webp', 0, 'lot-14-jeux-Game-Boy-GB-GBC-Japan', 2, '2025-05-29 11:15:09.451543'),
('/images/uploads/367a3bd5-efc2-43be-a81a-7b0d96702d09.jpg', 0, 'part0010', 8, '2025-05-29 11:17:30.976845'),
('/images/uploads/0f5db4dc-49ba-499f-b8bc-6cad99df911b.jpg', 1, 's-l1200 (2)', 8, '2025-05-29 11:17:30.980387'),
('/images/uploads/d70adf56-28cc-4143-b529-f27883071202.jpg', 0, 'cartou10', 6, '2025-05-29 11:18:48.004164'),
('/images/uploads/81bb9643-672e-4eb4-a043-5541ebf6ceeb.jpg', 0, 's-l400 (3)', 1, '2025-05-29 11:20:24.690347'),
('/images/uploads/9a7040e9-aa3d-452f-b977-d6005de881e2.jpg', 1, 's-l1200 (3)', 1, '2025-05-29 11:20:24.696049'),
('/images/uploads/6f7542d7-8fe3-4031-a034-6b811ee053e7.webp', 0, 'nintendo_scope_boite', 12, '2025-05-29 11:23:11.808215'),
('/images/uploads/9d436aea-1849-48f8-a5fc-c525adc6b20a.jpg', 1, 's-l1200 (3)', 12, '2025-05-29 11:23:11.823083'),
('/images/uploads/58a2d341-91c0-47ef-bf32-ebba8f55e232.jpg', 2, 'Super_nes_mouse_01', 12, '2025-05-29 11:23:11.825283'),
('/images/uploads/e38593ea-f7c4-40a3-9632-78603a65aacc.png', 3, 'X2Pzjn1', 12, '2025-05-29 11:23:11.832304');


INSERT INTO favorites (user_id, listing_id, favorited_date) VALUES
(1, 3, CURRENT_TIMESTAMP - INTERVAL '2 days'),
(1, 5, CURRENT_TIMESTAMP - INTERVAL '1 day 3 hours'),
(1, 9, CURRENT_TIMESTAMP - INTERVAL '5 hours'),
(2, 1, CURRENT_TIMESTAMP - INTERVAL '3 days'),
(2, 4, CURRENT_TIMESTAMP - INTERVAL '2 days 6 hours'),
(2, 10, CURRENT_TIMESTAMP - INTERVAL '8 hours'),
(3, 2, CURRENT_TIMESTAMP - INTERVAL '1 day'),
(3, 6, CURRENT_TIMESTAMP - INTERVAL '4 days'),
(3, 11, CURRENT_TIMESTAMP - INTERVAL '2 hours'),
(4, 3, CURRENT_TIMESTAMP - INTERVAL '2 days 5 hours'),
(4, 7, CURRENT_TIMESTAMP - INTERVAL '12 hours'),
(4, 12, CURRENT_TIMESTAMP - INTERVAL '6 hours'),
(5, 1, CURRENT_TIMESTAMP - INTERVAL '1 day 2 hours'),
(5, 5, CURRENT_TIMESTAMP - INTERVAL '3 days 7 hours'),
(5, 8, CURRENT_TIMESTAMP - INTERVAL '9 hours'),
(1, 2, CURRENT_TIMESTAMP - INTERVAL '4 days'),
(2, 6, CURRENT_TIMESTAMP - INTERVAL '5 days'),
(3, 8, CURRENT_TIMESTAMP - INTERVAL '1 day 5 hours'),
(4, 9, CURRENT_TIMESTAMP - INTERVAL '7 hours'),
(5, 12, CURRENT_TIMESTAMP - INTERVAL '3 hours');

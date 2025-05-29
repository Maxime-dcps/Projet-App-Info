CREATE TABLE users (
    user_id SERIAL PRIMARY KEY,
    username VARCHAR(50) NOT NULL UNIQUE,
    email VARCHAR(100) NOT NULL UNIQUE,
    password_hash VARCHAR(100) NOT NULL,
    salt VARCHAR(50) NOT NULL,
    role VARCHAR(50) NOT NULL,
    creation_date TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- Table to categorize listings
CREATE TABLE tags (
    tag_id SERIAL PRIMARY KEY,
    label VARCHAR(50) NOT NULL UNIQUE
);

-- Table for the announcements
CREATE TABLE listings (
    listing_id SERIAL PRIMARY KEY,
    title VARCHAR(100) NOT NULL,
    description TEXT,
    price DECIMAL(15,2) NOT NULL CHECK (price >= 0),
    is_available BOOLEAN NOT NULL DEFAULT TRUE,         -- State of the announcement (active, inactive)
    user_id INT NOT NULL,                               -- Announcement's author
    creation_date TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,                
    
    FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE CASCADE
);

-- Table for images associated with listings
CREATE TABLE images (
    image_id SERIAL PRIMARY KEY,
    file_path VARCHAR(255) NOT NULL,        -- Path to the image
    image_order INT NOT NULL DEFAULT 0,     -- Order of display
    alt_text VARCHAR(255),                  
    listing_id INT NOT NULL,                
    upload_date TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    FOREIGN KEY (listing_id) REFERENCES listings(listing_id) ON DELETE CASCADE
);

-- Join table between listings and tags
CREATE TABLE listing_tags (
    tag_id INT NOT NULL,                    
    listing_id INT NOT NULL,

    PRIMARY KEY (tag_id, listing_id),       -- Composite primary key
    FOREIGN KEY (tag_id) REFERENCES tags(tag_id) ON DELETE CASCADE,
    FOREIGN KEY (listing_id) REFERENCES listings(listing_id) ON DELETE CASCADE
);

-- Table for users' favorites
CREATE TABLE favorites (
    user_id INT NOT NULL,                   
    listing_id INT NOT NULL,               
    favorited_date TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP, -- Date the user added listings to favorites

    PRIMARY KEY (user_id, listing_id),                           -- Composite primary key
    FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE CASCADE,
    FOREIGN KEY (listing_id) REFERENCES listings(listing_id) ON DELETE CASCADE
);
Create database SkincareShop;

Use SkincareShop;

CREATE TABLE skin_types (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(100) UNIQUE NOT NULL,
    description NVARCHAR(MAX)
);
CREATE TABLE roles (
    id INT IDENTITY(1,1) PRIMARY KEY,
    roleName NVARCHAR(100) UNIQUE NOT NULL
);
CREATE TABLE users (
    id INT IDENTITY(1,1) PRIMARY KEY,
    fullName NVARCHAR(255) NOT NULL,
    email NVARCHAR(255) UNIQUE NOT NULL,
    phone NVARCHAR(20) UNIQUE,
    passwordHash NVARCHAR(255) NOT NULL,
    address NVARCHAR(MAX),
    birthdate DATE,
    skinTypeId INT FOREIGN KEY REFERENCES skin_types(id),
    loyaltyPoints INT DEFAULT 0,
    roleId INT FOREIGN KEY REFERENCES roles(id),
    createdAt DATETIME DEFAULT GETDATE(),
    updatedAt DATETIME DEFAULT GETDATE()
);



CREATE TABLE categories (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(255) UNIQUE NOT NULL,
    isActive BIT NOT NULL,
    description NVARCHAR(MAX)
);

CREATE TABLE brands (
    id INT IDENTITY(1,1) PRIMARY KEY,
    brandName NVARCHAR(255) NOT NULL,
    countryCode NVARCHAR(10),
    logo NVARCHAR(255),
    isActive BIT NOT NULL,
    createdAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE products (
    id INT IDENTITY(1,1) PRIMARY KEY,
    categoryId INT FOREIGN KEY REFERENCES categories(id),
    brandId INT FOREIGN KEY REFERENCES brands(id),
    name NVARCHAR(255) NOT NULL,
    description NVARCHAR(MAX),
    price DECIMAL(10,2) NOT NULL,
    stock INT DEFAULT 0,
    rating FLOAT DEFAULT 0,
    imageUrl NVARCHAR(MAX),
    createdAt DATETIME DEFAULT GETDATE(),
    updatedAt DATETIME DEFAULT GETDATE()
);



CREATE TABLE orders (
    id INT IDENTITY(1,1) PRIMARY KEY,
    userId INT FOREIGN KEY REFERENCES users(id),
    totalPrice DECIMAL(10,2) NOT NULL,
    status NVARCHAR(50) CHECK (status IN ('pending', 'confirmed', 'shipped', 'delivered', 'cancelled')) DEFAULT 'pending',
    createdAt DATETIME DEFAULT GETDATE(),
    updatedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE order_items (
    id INT IDENTITY(1,1) PRIMARY KEY,
    orderId INT FOREIGN KEY REFERENCES orders(id) ON DELETE CASCADE,
    productId INT FOREIGN KEY REFERENCES products(id),
    quantity INT NOT NULL,
    unitPrice DECIMAL(10,2) NOT NULL
);

CREATE TABLE payments (
    id INT IDENTITY(1,1) PRIMARY KEY,
    orderId INT FOREIGN KEY REFERENCES orders(id) ON DELETE CASCADE,
    paymentMethod NVARCHAR(50) CHECK (paymentMethod IN ('credit_card', 'paypal', 'cod', 'bank_transfer')),
    paymentStatus NVARCHAR(50) CHECK (paymentStatus IN ('pending', 'completed', 'failed', 'refunded')) DEFAULT 'pending',
    transactionId NVARCHAR(255) UNIQUE,
    amount DECIMAL(10,2) NOT NULL,
    createdAt DATETIME DEFAULT GETDATE()
);


CREATE TABLE skin_quiz_questions (
    id INT IDENTITY(1,1) PRIMARY KEY,
    question NVARCHAR(MAX) NOT NULL
);

CREATE TABLE skin_quiz_answers (
    id INT IDENTITY(1,1) PRIMARY KEY,
    questionId INT FOREIGN KEY REFERENCES skin_quiz_questions(id),
    answer NVARCHAR(MAX) NOT NULL,
    skinTypeId INT FOREIGN KEY REFERENCES skin_types(id)
);

CREATE TABLE user_skin_tests (
    id INT IDENTITY(1,1) PRIMARY KEY,
    userId INT FOREIGN KEY REFERENCES users(id),
    skinTypeId INT FOREIGN KEY REFERENCES skin_types(id),
    createdAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE product_skin_types (
    productId INT FOREIGN KEY REFERENCES products(id),
    skinTypeId INT FOREIGN KEY REFERENCES skin_types(id),
    PRIMARY KEY (productId, skinTypeId)
);


CREATE TABLE feedbacks (
    id INT IDENTITY(1,1) PRIMARY KEY,
    userId INT FOREIGN KEY REFERENCES users(id),
    productId INT FOREIGN KEY REFERENCES products(id),
    rating INT CHECK (rating BETWEEN 1 AND 5),
    comment NVARCHAR(MAX),
    createdAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE discounts (
    id INT IDENTITY(1,1) PRIMARY KEY,
    code NVARCHAR(50) UNIQUE NOT NULL,
    discountType NVARCHAR(50) CHECK (discountType IN ('percentage', 'fixed')),
    discountValue DECIMAL(10,2) NOT NULL,
    expirationDate DATE,
    minOrderValue DECIMAL(10,2),
    maxDiscount DECIMAL(10,2),
    createdAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE loyalty_points (
    id INT IDENTITY(1,1) PRIMARY KEY,
    userId INT FOREIGN KEY REFERENCES users(id),
    points INT NOT NULL,
    createdAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE content (
    id INT IDENTITY(1,1) PRIMARY KEY,
    title NVARCHAR(255) NOT NULL,
    contentType NVARCHAR(50) CHECK (contentType IN ('blog', 'news', 'faq')),
    content NVARCHAR(MAX),
    authorId INT FOREIGN KEY REFERENCES users(id),
    imageUrl NVARCHAR(MAX),
    isPublished BIT DEFAULT 1,
    publishedAt DATETIME,
    createdAt DATETIME DEFAULT GETDATE(),
    updatedAt DATETIME DEFAULT GETDATE()
);


-- Insert roles
--INSERT INTO roles (roleName) VALUES 
--('Admin'),
--('Staff'),
--('Customer');

---- Insert skin types
--INSERT INTO skin_types (name, description) VALUES 
--('Normal', 'Balanced skin with minimal issues'),
--('Dry', 'Skin lacks moisture and feels tight'),
--('Oily', 'Excess oil production leading to shine'),
--('Combination', 'Mix of oily and dry areas'),
--('Sensitive', 'Easily irritated and reactive skin');

---- Insert users
--INSERT INTO users (fullName, email, phone, passwordHash, address, birthdate, skinTypeId, loyaltyPoints, roleId) VALUES 
--('Alice Johnson', 'alice@example.com', '123456789', 'hashedpassword1', '123 Street, City', '1995-06-15', 1, 100, 3),
--('Bob Smith', 'bob@example.com', '987654321', 'hashedpassword2', '456 Avenue, City', '1990-09-25', 2, 200, 3),
--('Admin User', 'admin@example.com', '555555555', 'hashedadminpass', 'Admin Office', '1985-01-10', NULL, 0, 1);

---- Insert categories
--INSERT INTO categories (name, isActive, description) VALUES 
--('Cleanser', 1, 'Face wash and cleansing products'),
--('Toner', 1, 'Balancing toners for skincare'),
--('Serum', 1, 'Skin serums for hydration and treatment'),
--('Moisturizer', 1, 'Creams and lotions for hydration'),
--('Sunscreen', 1, 'Sun protection products');

---- Insert brands
--INSERT INTO brands (brandName, countryCode, logo, isActive) VALUES 
--('Brand A', 'US', 'brand_a_logo.png', 1),
--('Brand B', 'FR', 'brand_b_logo.png', 1),
--('Brand C', 'KR', 'brand_c_logo.png', 1);

---- Insert products
--INSERT INTO products (categoryId, brandId, name, description, price, stock, rating, imageUrl) VALUES 
--(1, 1, 'Gentle Cleanser', 'A mild face wash', 15.99, 50, 4.5, 'cleanser1.png'),
--(2, 2, 'Hydrating Toner', 'Balances skin pH', 18.99, 40, 4.7, 'toner1.png'),
--(3, 3, 'Vitamin C Serum', 'Brightens and smoothens skin', 29.99, 30, 4.8, 'serum1.png'),
--(4, 1, 'Moisturizing Cream', 'Deep hydration for dry skin', 24.99, 20, 4.6, 'moisturizer1.png'),
--(5, 2, 'SPF 50 Sunscreen', 'High protection sunscreen', 19.99, 60, 4.5, 'sunscreen1.png');

---- Insert product-skin type relationships
--INSERT INTO product_skin_types (productId, skinTypeId) VALUES 
--(1, 1), (1, 3), -- Cleanser for Normal and Oily skin
--(2, 1), (2, 2), (2, 5), -- Toner for Normal, Dry, and Sensitive skin
--(3, 4), (3, 5), -- Serum for Combination and Sensitive skin
--(4, 2), (4, 3), (4, 4), -- Moisturizer for Dry, Oily, and Combination skin
--(5, 1), (5, 2), (5, 3), (5, 4), (5, 5); -- Sunscreen for all skin types

---- Insert sample orders
--INSERT INTO orders (userId, totalPrice, status) VALUES 
--(1, 44.98, 'confirmed'),
--(2, 19.99, 'pending');

---- Insert order items
--INSERT INTO order_items (orderId, productId, quantity, unitPrice) VALUES 
--(1, 1, 1, 15.99),
--(1, 3, 1, 29.99),
--(2, 5, 1, 19.99);

---- Insert payments
--INSERT INTO payments (orderId, paymentMethod, paymentStatus, transactionId, amount) VALUES 
--(1, 'credit_card', 'completed', 'TXN12345', 44.98),
--(2, 'paypal', 'pending', 'TXN67890', 19.99);

---- Insert feedbacks
--INSERT INTO feedbacks (userId, productId, rating, comment) VALUES 
--(1, 1, 5, 'Great cleanser, very gentle!'),
--(2, 3, 4, 'Good serum but a bit expensive.');

---- Insert discounts
--INSERT INTO discounts (code, discountType, discountValue, expirationDate, minOrderValue, maxDiscount) VALUES 
--('SUMMER10', 'percentage', 10.00, '2025-12-31', 50.00, 20.00),
--('WELCOME5', 'fixed', 5.00, '2025-06-30', NULL, NULL);

---- Insert loyalty points
--INSERT INTO loyalty_points (userId, points) VALUES 
--(1, 100),
--(2, 200);

---- Insert content (blogs, news, FAQs)
--INSERT INTO content (title, contentType, content, authorId, imageUrl, isPublished, publishedAt) VALUES 
--('Skincare Routine 101', 'blog', 'Learn the basics of skincare...', 1, 'blog1.png', 1, GETDATE()),
--('New Product Launch!', 'news', 'We are excited to introduce...', 2, 'news1.png', 1, GETDATE()),
--('How to choose the right toner?', 'faq', 'Toners vary based on skin type...', 1, NULL, 1, GETDATE());



USE SkincareShop;

-- ===============================
-- BASIC DATA: ROLES & SKIN TYPES
-- ===============================

-- Insert roles
INSERT INTO roles (roleName) VALUES 
('Admin'),
('Staff'),
('Customer');

-- Insert skin types
INSERT INTO skin_types (name, description) VALUES 
('Normal', 'Balanced skin with minimal issues'),
('Dry', 'Skin lacks moisture and feels tight'),
('Oily', 'Excess oil production leading to shine'),
('Combination', 'Mix of oily and dry areas'),
('Sensitive', 'Easily irritated and reactive skin');

-- Insert users
INSERT INTO users (fullName, email, phone, passwordHash, address, birthdate, skinTypeId, loyaltyPoints, roleId) VALUES 
('Alice Johnson', 'alice@example.com', '123456789', 'hashedpassword1', '123 Street, City', '1995-06-15', 1, 100, 3),
('Bob Smith', 'bob@example.com', '987654321', 'hashedpassword2', '456 Avenue, City', '1990-09-25', 2, 200, 3),
('Admin User', 'admin@example.com', '555555555', 'hashedadminpass', 'Admin Office', '1985-01-10', NULL, 0, 1);

-- ===============================
-- CATEGORIES
-- ===============================

-- Insert core categories
INSERT INTO categories (name, isActive, description) VALUES 
('Cleanser', 1, 'Face wash and cleansing products'),
('Toner', 1, 'Balancing toners for skincare'),
('Serum', 1, 'Skin serums for hydration and treatment'),
('Moisturizer', 1, 'Creams and lotions for hydration'),
('Sunscreen', 1, 'Sun protection products');

-- Insert additional categories
INSERT INTO categories (name, isActive, description) VALUES 
('Essence', 1, 'Lightweight treatment products to hydrate and prep skin'),
('Exfoliator', 1, 'Products that remove dead skin cells'),
('Eye Cream', 1, 'Specialized moisturizers for the delicate eye area'),
('Face Mask', 1, 'Intensive treatment products for various skin concerns'),
('Night Cream', 1, 'Richer formulas designed for overnight use');

-- ===============================
-- BRANDS
-- ===============================

-- Insert initial brands
INSERT INTO brands (brandName, countryCode, logo, isActive) VALUES 
('Brand A', 'US', 'brand_a_logo.png', 1),
('Brand B', 'FR', 'brand_b_logo.png', 1),
('Brand C', 'KR', 'brand_c_logo.png', 1);

-- Insert additional brands
INSERT INTO brands (brandName, countryCode, logo, isActive) VALUES 
('Klairs', 'KR', 'https://example.com/klairs.png', 1),
('COSRX', 'KR', 'https://example.com/cosrx.png', 1),
('CeraVe', 'US', 'https://example.com/cerave.png', 1),
('La Roche-Posay', 'FR', 'https://example.com/larocheposay.png', 1),
('The Ordinary', 'CA', 'https://example.com/theordinary.png', 1),
('L''Oréal Paris', 'FR', 'https://example.com/loreal.png', 1),
('Neutrogena', 'US', 'https://example.com/neutrogena.png', 1),
('Paula''s Choice', 'US', 'https://example.com/paulaschoice.png', 1),
('Olay', 'US', 'https://example.com/olay.png', 1),
('Cetaphil', 'US', 'https://example.com/cetaphil.png', 1),
('Eucerin', 'DE', 'https://example.com/eucerin.png', 1),
('Bioderma', 'FR', 'https://example.com/bioderma.png', 1),
('Aveeno', 'US', 'https://example.com/aveeno.png', 1),
('Kiehl''s', 'US', 'https://example.com/kiehls.png', 1),
('SK-II', 'JP', 'https://example.com/sk2.png', 1),
('Shiseido', 'JP', 'https://example.com/shiseido.png', 1),
('Drunk Elephant', 'US', 'https://example.com/drunkelephant.png', 1),
('Tatcha', 'JP', 'https://example.com/tatcha.png', 1),
('Estée Lauder', 'US', 'https://example.com/esteelauder.png', 1);

-- ===============================
-- PRODUCTS
-- ===============================

-- Insert initial products
INSERT INTO products (categoryId, brandId, name, description, price, stock, rating, imageUrl) VALUES 
(1, 1, 'Gentle Cleanser', 'A mild face wash', 15.99, 50, 4.5, 'cleanser1.png'),
(2, 2, 'Hydrating Toner', 'Balances skin pH', 18.99, 40, 4.7, 'toner1.png'),
(3, 3, 'Vitamin C Serum', 'Brightens and smoothens skin', 29.99, 30, 4.8, 'serum1.png'),
(4, 1, 'Moisturizing Cream', 'Deep hydration for dry skin', 24.99, 20, 4.6, 'moisturizer1.png'),
(5, 2, 'SPF 50 Sunscreen', 'High protection sunscreen', 19.99, 60, 4.5, 'sunscreen1.png');

-- Insert additional products
INSERT INTO products (categoryId, brandId, name, description, price, stock, rating, imageUrl) VALUES 
((SELECT id FROM categories WHERE name = 'Toner'), 
 (SELECT id FROM brands WHERE brandName = 'Klairs'), 
 'Klairs Supple Preparation Toner', 'A gentle, hydrating toner that balances skin pH', 22.99, 50, 4.7, 'klairs_toner.png'),

((SELECT id FROM categories WHERE name = 'Serum'), 
 (SELECT id FROM brands WHERE brandName = 'COSRX'), 
 'COSRX Advanced Snail 96 Mucin Power Essence', 'Hydrating essence with snail secretion filtrate for repair and hydration', 23.00, 45, 4.8, 'cosrx_snail.png'),

((SELECT id FROM categories WHERE name = 'Cleanser'), 
 (SELECT id FROM brands WHERE brandName = 'CeraVe'), 
 'CeraVe Hydrating Facial Cleanser', 'Gentle, non-foaming cleanser for normal to dry skin', 14.99, 100, 4.5, 'cerave_cleanser.png'),

((SELECT id FROM categories WHERE name = 'Cleanser'), 
 (SELECT id FROM brands WHERE brandName = 'La Roche-Posay'), 
 'La Roche-Posay Toleriane Hydrating Gentle Cleanser', 'Gentle cleanser for sensitive skin', 14.99, 80, 4.6, 'lrp_cleanser.png'),

((SELECT id FROM categories WHERE name = 'Serum'), 
 (SELECT id FROM brands WHERE brandName = 'The Ordinary'), 
 'The Ordinary Niacinamide 10% + Zinc 1%', 'Serum targeting blemishes and congestion', 6.50, 120, 4.3, 'ordinary_niacinamide.png'),

((SELECT id FROM categories WHERE name = 'Serum'), 
 (SELECT id FROM brands WHERE brandName = 'L''Oréal Paris'), 
 'L''Oréal Revitalift Hyaluronic Acid Serum', 'Hydrating serum with pure hyaluronic acid', 23.99, 60, 4.4, 'loreal_revitalift.png'),

((SELECT id FROM categories WHERE name = 'Moisturizer'), 
 (SELECT id FROM brands WHERE brandName = 'Neutrogena'), 
 'Neutrogena Hydro Boost Water Gel', 'Lightweight gel moisturizer with hyaluronic acid', 18.99, 90, 4.7, 'neutrogena_hydroboost.png'),

((SELECT id FROM categories WHERE name = 'Exfoliator'), 
 (SELECT id FROM brands WHERE brandName = 'Paula''s Choice'), 
 'Paula''s Choice 2% BHA Liquid Exfoliant', 'Leave-on exfoliant for unclogging pores', 29.99, 55, 4.9, 'pc_bha.png'),

((SELECT id FROM categories WHERE name = 'Moisturizer'), 
 (SELECT id FROM brands WHERE brandName = 'Olay'), 
 'Olay Regenerist Micro-Sculpting Cream', 'Anti-aging moisturizer for plumping skin', 28.99, 70, 4.5, 'olay_regenerist.png'),

((SELECT id FROM categories WHERE name = 'Essence'), 
 (SELECT id FROM brands WHERE brandName = 'SK-II'), 
 'SK-II Facial Treatment Essence', 'Luxury essence with Pitera for skin renewal', 99.99, 20, 4.8, 'skii_essence.png');

-- ===============================
-- PRODUCT-SKIN TYPE RELATIONSHIPS
-- ===============================

-- Insert initial product-skin type relationships
INSERT INTO product_skin_types (productId, skinTypeId) VALUES 
(1, 1), (1, 3), -- Gentle Cleanser for Normal and Oily skin
(2, 1), (2, 2), (2, 5), -- Hydrating Toner for Normal, Dry, and Sensitive skin
(3, 4), (3, 5), -- Vitamin C Serum for Combination and Sensitive skin
(4, 2), (4, 3), (4, 4), -- Moisturizing Cream for Dry, Oily, and Combination skin
(5, 1), (5, 2), (5, 3), (5, 4), (5, 5); -- SPF 50 Sunscreen for all skin types

-- Insert additional product-skin type relationships
INSERT INTO product_skin_types (productId, skinTypeId) VALUES
-- Klairs Toner is good for Normal, Dry and Sensitive skin
((SELECT id FROM products WHERE name = 'Klairs Supple Preparation Toner'), 1),
((SELECT id FROM products WHERE name = 'Klairs Supple Preparation Toner'), 2),
((SELECT id FROM products WHERE name = 'Klairs Supple Preparation Toner'), 5),

-- COSRX Snail Essence is good for all skin types
((SELECT id FROM products WHERE name = 'COSRX Advanced Snail 96 Mucin Power Essence'), 1),
((SELECT id FROM products WHERE name = 'COSRX Advanced Snail 96 Mucin Power Essence'), 2),
((SELECT id FROM products WHERE name = 'COSRX Advanced Snail 96 Mucin Power Essence'), 3),
((SELECT id FROM products WHERE name = 'COSRX Advanced Snail 96 Mucin Power Essence'), 4),
((SELECT id FROM products WHERE name = 'COSRX Advanced Snail 96 Mucin Power Essence'), 5),

-- CeraVe Cleanser is good for Normal, Dry and Sensitive skin
((SELECT id FROM products WHERE name = 'CeraVe Hydrating Facial Cleanser'), 1),
((SELECT id FROM products WHERE name = 'CeraVe Hydrating Facial Cleanser'), 2),
((SELECT id FROM products WHERE name = 'CeraVe Hydrating Facial Cleanser'), 5),

-- The Ordinary Niacinamide is good for Oily and Combination skin
((SELECT id FROM products WHERE name = 'The Ordinary Niacinamide 10% + Zinc 1%'), 3),
((SELECT id FROM products WHERE name = 'The Ordinary Niacinamide 10% + Zinc 1%'), 4);

-- ===============================
-- ORDERS AND ORDER ITEMS
-- ===============================

-- Insert sample orders
INSERT INTO orders (userId, totalPrice, status) VALUES 
(1, 44.98, 'confirmed'),
(2, 19.99, 'pending');

-- Insert order items
INSERT INTO order_items (orderId, productId, quantity, unitPrice) VALUES 
(1, 1, 1, 15.99),
(1, 3, 1, 29.99),
(2, 5, 1, 19.99);

-- ===============================
-- PAYMENTS
-- ===============================

-- Insert payments
INSERT INTO payments (orderId, paymentMethod, paymentStatus, transactionId, amount) VALUES 
(1, 'credit_card', 'completed', 'TXN12345', 44.98),
(2, 'paypal', 'pending', 'TXN67890', 19.99);

-- ===============================
-- FEEDBACK, DISCOUNTS, LOYALTY
-- ===============================

-- Insert feedbacks
INSERT INTO feedbacks (userId, productId, rating, comment) VALUES 
(1, 1, 5, 'Great cleanser, very gentle!'),
(2, 3, 4, 'Good serum but a bit expensive.');

-- Insert discounts
INSERT INTO discounts (code, discountType, discountValue, expirationDate, minOrderValue, maxDiscount) VALUES 
('SUMMER10', 'percentage', 10.00, '2025-12-31', 50.00, 20.00),
('WELCOME5', 'fixed', 5.00, '2025-06-30', NULL, NULL);

-- Insert loyalty points
INSERT INTO loyalty_points (userId, points) VALUES 
(1, 100),
(2, 200);

-- ===============================
-- CONTENT
-- ===============================

-- Insert content (blogs, news, FAQs)
INSERT INTO content (title, contentType, content, authorId, imageUrl, isPublished, publishedAt) VALUES 
('Skincare Routine 101', 'blog', 'Learn the basics of skincare...', 1, 'blog1.png', 1, GETDATE()),
('New Product Launch!', 'news', 'We are excited to introduce...', 2, 'news1.png', 1, GETDATE()),
('How to choose the right toner?', 'faq', 'Toners vary based on skin type...', 1, NULL, 1, GETDATE());

-- ===============================
-- SKIN QUIZ QUESTIONS & ANSWERS
-- ===============================

-- Add skin quiz questions
INSERT INTO skin_quiz_questions (question) VALUES 
('Vào mỗi buổi sáng thức dậy, bạn thấy da mình thế nào?'),
('Thực hiện rửa mặt với sữa rửa mặt của bạn với nước ấm. Từ 20 - 30'' sau, cảm nhận của da bạn là thế nào?'),
('Hãy nhìn kỹ xem lỗ chân lông trên da bạn ra sao?'),
('Từ nào dưới đây có thể miêu tả kết cấu da bạn?'),
('Vào buổi trưa, da bạn ở trình trạng nào? (Không dùng tay, chỉ soi gương để đoán)'),
('Bạn có thường xuyên nặn mụn trứng cá?');

-- Add skin quiz answers (mapping to appropriate skin types)
-- Question 1
INSERT INTO skin_quiz_answers (questionId, answer, skinTypeId) VALUES 
(IDENT_CURRENT('skin_quiz_questions') - 5, 'Bình thường. Không có sự khác biệt so với trước khi ngủ', 1), -- Normal
(IDENT_CURRENT('skin_quiz_questions') - 5, 'Nhiều dầu. Tập trung ở mũi và trán', 3), -- Oily
(IDENT_CURRENT('skin_quiz_questions') - 5, 'Khô và nẻ', 2), -- Dry
(IDENT_CURRENT('skin_quiz_questions') - 5, 'Tấy đỏ Bong da', 5); -- Sensitive

-- Question 2
INSERT INTO skin_quiz_answers (questionId, answer, skinTypeId) VALUES 
(IDENT_CURRENT('skin_quiz_questions') - 4, 'Tốt', 1), -- Normal
(IDENT_CURRENT('skin_quiz_questions') - 4, 'Vẫn còn nhiều dầu', 3), -- Oily
(IDENT_CURRENT('skin_quiz_questions') - 4, 'Khô và ráp', 2), -- Dry
(IDENT_CURRENT('skin_quiz_questions') - 4, 'Mẫn đỏ', 5); -- Sensitive

-- Question 3
INSERT INTO skin_quiz_answers (questionId, answer, skinTypeId) VALUES 
(IDENT_CURRENT('skin_quiz_questions') - 3, 'Nhỏ', 1), -- Normal
(IDENT_CURRENT('skin_quiz_questions') - 3, 'Lớn', 3), -- Oily
(IDENT_CURRENT('skin_quiz_questions') - 3, 'Khô', 2), -- Dry
(IDENT_CURRENT('skin_quiz_questions') - 3, 'Đỏ', 5); -- Sensitive

-- Question 4
INSERT INTO skin_quiz_answers (questionId, answer, skinTypeId) VALUES 
(IDENT_CURRENT('skin_quiz_questions') - 2, 'Mềm mịn', 1), -- Normal
(IDENT_CURRENT('skin_quiz_questions') - 2, 'Nhiều dầu', 3), -- Oily
(IDENT_CURRENT('skin_quiz_questions') - 2, 'Hơi khô', 2), -- Dry
(IDENT_CURRENT('skin_quiz_questions') - 2, 'Mỏng, lộ đường mạch máu', 5); -- Sensitive

-- Question 5
INSERT INTO skin_quiz_answers (questionId, answer, skinTypeId) VALUES 
(IDENT_CURRENT('skin_quiz_questions') - 1, 'Như buổi sáng', 1), -- Normal
(IDENT_CURRENT('skin_quiz_questions') - 1, 'Sáng', 3), -- Oily
(IDENT_CURRENT('skin_quiz_questions') - 1, 'Khô', 2), -- Dry
(IDENT_CURRENT('skin_quiz_questions') - 1, 'Nhạy cảm', 5); -- Sensitive

-- Question 6
INSERT INTO skin_quiz_answers (questionId, answer, skinTypeId) VALUES 
(IDENT_CURRENT('skin_quiz_questions'), 'Thỉnh thoảng', 1), -- Normal
(IDENT_CURRENT('skin_quiz_questions'), 'Thường xuyên, đặc biệt vào chu kỳ', 3), -- Oily
(IDENT_CURRENT('skin_quiz_questions'), 'Không bao giờ', 2), -- Dry
(IDENT_CURRENT('skin_quiz_questions'), 'Chỉ khi trang điểm', 4); -- Combination
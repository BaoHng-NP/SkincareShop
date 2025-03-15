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
    createdAt DATETIME DEFAULT GETDATE()
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
    createdAt DATETIME DEFAULT GETDATE()
);
-------------------------------------------
CREATE TABLE product_details (
    id INT IDENTITY(1,1) PRIMARY KEY,
    productId INT FOREIGN KEY REFERENCES products(id) ON DELETE CASCADE,
    manufactureDate DATE,
    expiryDate DATE,
    quantity INT DEFAULT 0,
    createdDate DATETIME DEFAULT GETDATE()  -- Thêm trường mới này
);

CREATE TABLE orders (
    id INT IDENTITY(1,1) PRIMARY KEY,
    userId INT FOREIGN KEY REFERENCES users(id),
    totalPrice DECIMAL(10,2) NOT NULL,
    status NVARCHAR(50) CHECK (status IN ('pending', 'confirmed', 'shipped', 'delivered', 'cancelled')) DEFAULT 'pending',
    createdAt DATETIME DEFAULT GETDATE()
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
    requiredPoints INT NOT NULL DEFAULT 0,  -- Điểm cần để đổi voucher
    expirationDate DATE,
    minOrderValue DECIMAL(10,2),
    maxDiscount DECIMAL(10,2),
    createdAt DATETIME DEFAULT GETDATE()
);
CREATE TABLE user_vouchers (
    id INT IDENTITY(1,1) PRIMARY KEY,
    userId INT FOREIGN KEY REFERENCES users(id),
    discountId INT FOREIGN KEY REFERENCES discounts(id),
    redeemedAt DATETIME DEFAULT GETDATE()
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
    createdAt DATETIME DEFAULT GETDATE()
);


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
('Alice Johnson', 'alice@gmail.com', '123456789', 'hashedpassword1', '123 Street, City', '1995-06-15', 1, 30, 3),
('Bob Smith', 'bob@gmail.com', '987654321', 'hashedpassword2', '456 Avenue, City', '1990-09-25', 2, 20, 3),
('Admin User', 'admin@gmail.com', '555555555', '12345678', 'Admin Office', '1985-01-10', NULL, 0, 1),
('Staff User', 'staff@gmail.com', '666666666', '12345678', 'Staff Office', '1985-01-10', NULL, 0, 2);

-- ===============================
-- CATEGORIES
-- ===============================

-- Insert core categories
INSERT INTO categories (name, isActive, description) VALUES 
('Cleanser', 1, 'Face wash and cleansing products'),
('Toner', 1, 'Balancing toners for skincare'),
('Serum', 1, 'Skin serums for hydration and treatment'),
('Moisturizer', 1, 'Creams and lotions for hydration'),
('Sunscreen', 1, 'Sun protection products'),
('Essence', 1, 'Lightweight treatment products to hydrate and prep skin'),
('Exfoliator', 1, 'Products that remove dead skin cells'),
('Eye Cream', 1, 'Specialized moisturizers for the delicate eye area'),
('Face Mask', 1, 'Intensive treatment products for various skin concerns'),
('Night Cream', 1, 'Richer formulas designed for overnight use');


-- ===============================
-- BRANDS
-- ===============================

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

-- Insert additional products
INSERT INTO products (categoryId, brandId, name, description, price, stock, rating, imageUrl) VALUES 
((SELECT id FROM categories WHERE name = 'Toner'), 
 (SELECT id FROM brands WHERE brandName = 'Klairs'), 
 'Klairs Supple Preparation Toner', 'A gentle, hydrating toner that balances skin pH', 350000, 50, 4.7, 
 'https://klairsvietnam.vn/wp-content/uploads/2020/07/nuoc-hoa-hong-khong-mui-klairs.jpg'),

((SELECT id FROM categories WHERE name = 'Serum'), 
 (SELECT id FROM brands WHERE brandName = 'COSRX'), 
 'COSRX Advanced Snail 96 Mucin Power Essence', 'Hydrating essence with snail secretion filtrate for repair and hydration', 420000, 45, 4.8, 
 'https://media.loveitopcdn.com/2534/thumb/tinh-chat-oc-sen-giup-duong-am-va-phuc-hoi-lan-da-cosrx-snail-96-mucin-power-essence-100ml-7.jpg'),

((SELECT id FROM categories WHERE name = 'Cleanser'), 
 (SELECT id FROM brands WHERE brandName = 'CeraVe'), 
 'CeraVe Hydrating Facial Cleanser', 'Gentle, non-foaming cleanser for normal to dry skin', 245000, 100, 4.5, 
 'https://bizweb.dktcdn.net/100/407/286/products/cerave-hydrating-facial-cleanser.jpg?v=1621057598607'),

((SELECT id FROM categories WHERE name = 'Cleanser'), 
 (SELECT id FROM brands WHERE brandName = 'La Roche-Posay'), 
 'La Roche-Posay Toleriane Hydrating Gentle Cleanser', 'Gentle cleanser for sensitive skin', 295000, 80, 4.6, 
 'https://down-vn.img.susercontent.com/file/vn-11134201-7qukw-lj3qquih0l3m8b'),

((SELECT id FROM categories WHERE name = 'Serum'), 
 (SELECT id FROM brands WHERE brandName = 'The Ordinary'), 
 'The Ordinary Niacinamide 10% + Zinc 1%', 'Serum targeting blemishes and congestion', 199000, 120, 4.3, 
 'https://ordinary.com.vn/wp-content/uploads/2020/09/The-Ordinary-Niacinamide10-Zinc-1-510x455.jpg'),

((SELECT id FROM categories WHERE name = 'Serum'), 
 (SELECT id FROM brands WHERE brandName = 'L''Oréal Paris'), 
 'L''Oréal Revitalift Hyaluronic Acid Serum', 'Hydrating serum with pure hyaluronic acid', 450000, 60, 4.4, 
 'https://media.hcdn.vn/catalog/product/g/o/google-shopping-tinh-chat-l-oreal-hyaluronic-acid-cap-am-sang-da-30ml-1.jpg'),

((SELECT id FROM categories WHERE name = 'Moisturizer'), 
 (SELECT id FROM brands WHERE brandName = 'Neutrogena'), 
 'Neutrogena Hydro Boost Water Gel', 'Lightweight gel moisturizer with hyaluronic acid', 375000, 90, 4.7, 
 'https://product.hstatic.net/1000006063/product/dd_gel_b2879746c8d84a22856364fe9f6cbf1a_1024x1024_d012fe69b2ef4492b80518b6023b210f_1024x1024.jpg'),

((SELECT id FROM categories WHERE name = 'Exfoliator'), 
 (SELECT id FROM brands WHERE brandName = 'Paula''s Choice'), 
 'Paula''s Choice 2% BHA Liquid Exfoliant', 'Leave-on exfoliant for unclogging pores', 550000, 55, 4.9, 
 'https://paulaschoice.vn/wp-content/uploads/2019/08/2016-bha-9122020.jpg.webp'),

((SELECT id FROM categories WHERE name = 'Moisturizer'), 
 (SELECT id FROM brands WHERE brandName = 'Olay'), 
 'Olay Regenerist Micro-Sculpting Cream', 'Anti-aging moisturizer for plumping skin', 650000, 70, 4.5, 
 'https://www.ubuy.vn/productimg/?image=aHR0cHM6Ly9tLm1lZGlhLWFtYXpvbi5jb20vaW1hZ2VzL0kvNzFNamVTd3ZHK0wuX1NMMTUwMF8uanBn.jpg'),

((SELECT id FROM categories WHERE name = 'Essence'), 
 (SELECT id FROM brands WHERE brandName = 'SK-II'), 
 'SK-II Facial Treatment Essence', 'Luxury essence with Pitera for skin renewal', 699000, 20, 4.8, 
 'https://sieuthilamdep.com/images/detailed/10/nuoc-than-sk-ii-facial-treatment-essence-04.jpg'),

-- Thêm sản phẩm mới với các URL hình ảnh còn lại
((SELECT id FROM categories WHERE name = 'Sunscreen'), 
 (SELECT id FROM brands WHERE brandName = 'La Roche-Posay'), 
 'La Roche-Posay Anthelios SPF 50+ Sunscreen', 'High protection sunscreen for sensitive skin', 420000, 40, 4.7, 
 'https://cosmocosmetics.ae/cdn/shop/files/SUN-SCREEN-LOTION-SPF-50_UVB_UVA-100g.jpg?v=1725449166'),

((SELECT id FROM categories WHERE name = 'Moisturizer'), 
 (SELECT id FROM brands WHERE brandName = 'CeraVe'), 
 'CeraVe Moisturizing Cream', 'Rich moisturizer for dry to very dry skin', 320000, 65, 4.8, 
 'https://edbeauty.vn/wp-content/uploads/2024/10/Moisturizing-Cream-01.png'),

((SELECT id FROM categories WHERE name = 'Serum'), 
 (SELECT id FROM brands WHERE brandName = 'Drunk Elephant'), 
 'Radha Beauty Vitamin C Serum', 'Brightening and anti-aging serum with vitamin C', 450000, 30, 4.5, 
 'https://images-cdn.ubuy.co.in/633ac716946c6908770eec0a-radha-beauty-natural-vitamin-c-serum-for.jpg');

-- Add this after all product insertions
-- Insert product details
-- Cập nhật câu INSERT product_details để bao gồm createdDate
INSERT INTO product_details (productId, manufactureDate, expiryDate, quantity, createdDate) VALUES
(1, '2023-01-15', '2025-01-15', 50, '2023-01-15'),
(2, '2023-02-10', '2025-02-10', 45, '2023-02-10'),
(3, '2023-01-20', '2025-01-20', 100, '2023-01-20'),
(4, '2023-03-05', '2025-03-05', 80, '2023-03-05'),
(5, '2023-02-15', '2026-02-15', 120, '2023-02-15'),
(6, '2023-03-10', '2025-03-10', 60, '2023-03-10'),
(7, '2023-04-05', '2025-04-05', 90, '2023-04-05'),
(8, '2023-02-25', '2025-02-25', 55, '2023-02-25'),
(9, '2023-01-30', '2025-01-30', 70, '2023-01-30'),
(10, '2023-03-15', '2025-03-15', 20, '2023-03-15'),
(11, '2023-05-15', '2025-05-15', 40, '2023-05-15'),
(12, '2023-06-10', '2025-06-10', 65, '2023-06-10'),
(13, '2023-07-20', '2025-07-20', 30, '2023-07-20');

-- ===============================
-- PRODUCT-SKIN TYPE RELATIONSHIPS
-- ===============================

-- Insert initial product-skin type relationships
INSERT INTO product_skin_types (productId, skinTypeId) VALUES 
(1, 2), (1, 4), (1, 5), 
(2, 2), (2, 4), (2, 5),
(3, 1), (3, 2), 
(4, 5), 
(5, 1), 
(6, 2), (6, 4),
(7, 1), (7, 3),
(8, 1),
(9, 1), (9, 4), (9, 5),
(10, 1),
(11, 5), (11, 1), -- Sunscreen
(12, 2), (12, 5), -- Moisturizing Cream
(13, 1), (13, 4); -- Vitamin C Serum


-- ===============================
-- FEEDBACK, DISCOUNTS, LOYALTY
-- ===============================

-- Insert feedbacks
INSERT INTO feedbacks (userId, productId, rating, comment) VALUES 
(1, 1, 5, 'Great cleanser, very gentle!'),
(2, 3, 4, 'Good serum but a bit expensive.');

-- Insert discounts
INSERT INTO discounts (code, discountType, discountValue, requiredPoints, expirationDate, minOrderValue, maxDiscount) VALUES 
-- Vouchers cố định bằng VND
('VOUCHER30K', 'fixed', 30000, 30, '2025-12-31', 100000, NULL),
('VOUCHER50K', 'fixed', 50000, 50, '2025-12-31', 150000, NULL),
('VOUCHER100K', 'fixed', 100000, 100, '2025-12-31', 300000, NULL),
-- Voucher phần trăm
('SUMMER10', 'percentage', 10.00, 0, '2025-12-31', 100000, 100000),
-- Voucher cố định cho người mới
('WELCOME5', 'fixed', 20000, 0, '2025-06-30', 50000, NULL);
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
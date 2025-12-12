-- Очистка таблиц (в правильном порядке из-за внешних ключей)
DELETE FROM "Reviews";
DELETE FROM "Payments";
DELETE FROM "Bookings";
DELETE FROM "Users";
DELETE FROM "Rooms";
DELETE FROM "UserTypes";

-- 1. Заполнение UserTypes (2 записи)
INSERT INTO "UserTypes" ("Id", "Type", "CreatedAt", "UpdatedAt", "IsDeleted", "DeletedAt")
VALUES 
    (gen_random_uuid(), 'Администратор', NOW() - interval '30 days', NULL, false, NULL),
    (gen_random_uuid(), 'Пользователь', NOW() - interval '29 days', NULL, false, NULL);

-- 2. Заполнение Rooms (10 записей)
INSERT INTO "Rooms" ("Id", "Number", "Class", "Description", "PricePerNight", "CreatedAt", "UpdatedAt", "IsDeleted", "DeletedAt")
VALUES 
    (gen_random_uuid(), '101', 1, 'Стандартный номер с видом на сад', 2500.00, NOW() - interval '28 days', NULL, false, NULL),
    (gen_random_uuid(), '102', 1, 'Стандартный номер с двумя односпальными кроватями', 2400.00, NOW() - interval '27 days', NULL, false, NULL),
    (gen_random_uuid(), '201', 2, 'Комфорт номер с балконом', 3500.00, NOW() - interval '26 days', NULL, false, NULL),
    (gen_random_uuid(), '202', 2, 'Комфорт номер с джакузи', 4000.00, NOW() - interval '25 days', NULL, false, NULL),
    (gen_random_uuid(), '301', 3, 'Люкс с гостиной зоной', 6000.00, NOW() - interval '24 days', NULL, false, NULL),
    (gen_random_uuid(), '302', 3, 'Президентский люкс', 8500.00, NOW() - interval '23 days', NULL, false, NULL),
    (gen_random_uuid(), '103', 1, 'Стандартный номер для некурящих', 2300.00, NOW() - interval '22 days', NULL, false, NULL),
    (gen_random_uuid(), '203', 2, 'Комфорт номер семейный', 3800.00, NOW() - interval '21 days', NULL, false, NULL),
    (gen_random_uuid(), '303', 3, 'Люкс с панорамным видом', 7500.00, NOW() - interval '20 days', NULL, false, NULL),
    (gen_random_uuid(), '401', 4, 'Делюкс с камином', 9500.00, NOW() - interval '19 days', NULL, false, NULL);

-- 3. Заполнение Users (5 записей)
INSERT INTO "Users" ("Id", "Name", "Email", "Password", "UserTypeId", "CreatedAt", "UpdatedAt", "IsDeleted", "DeletedAt")
WITH user_types_cte AS (
    SELECT "Id", "Type" FROM "UserTypes"
)
SELECT 
    gen_random_uuid(),
    user_data.name,
    user_data.email,
    user_data.password,
    (SELECT "Id" FROM user_types_cte WHERE "Type" = user_data.user_type),
    NOW() - (interval '1 day' * user_data.days_ago),
    NULL,
    false,
    NULL
FROM (VALUES
    ('Иванов Иван Иванович', 'admin@hotel.com', 'hashed_password_1', 'Администратор', 15),
    ('Петров Петр Петрович', 'petrov@example.com', 'hashed_password_2', 'Пользователь', 14),
    ('Сидорова Анна Владимировна', 'sidorova@example.com', 'hashed_password_3', 'Пользователь', 13),
    ('Кузнецов Алексей Сергеевич', 'kuznetsov@example.com', 'hashed_password_4', 'Пользователь', 12),
    ('Морозова Екатерина Дмитриевна', 'morozova@example.com', 'hashed_password_5', 'Пользователь', 11)
) AS user_data(name, email, password, user_type, days_ago);

-- Создаем временные таблицы с ID для связей
WITH 
regular_users_cte AS (
    SELECT "Id", ROW_NUMBER() OVER (ORDER BY "CreatedAt") as rn 
    FROM "Users" 
    WHERE "Email" != 'admin@hotel.com'
),
rooms_cte AS (
    SELECT "Id", ROW_NUMBER() OVER (ORDER BY "Number") as rn 
    FROM "Rooms"
),
booking_dates AS (
    SELECT 
        generate_series(1, 20) as booking_num,
        (DATE '2024-01-01' + (interval '15 days' * (generate_series(1, 20) - 1))) as check_in_date,
        (DATE '2024-01-01' + (interval '15 days' * (generate_series(1, 20) - 1)) + interval '5 days') as check_out_date
)

-- 4. Заполнение Bookings (20 записей)
INSERT INTO "Bookings" ("Id", "CheckInDate", "CheckOutDate", "RoomId", "UserId", "CreatedAt", "UpdatedAt", "IsDeleted", "DeletedAt")
SELECT 
    gen_random_uuid(),
    bd.check_in_date,
    bd.check_out_date,
    (SELECT "Id" FROM rooms_cte WHERE rn = ((bd.booking_num - 1) % 10) + 1),
    (SELECT "Id" FROM regular_users_cte WHERE rn = ((bd.booking_num - 1) % 4) + 1),
    bd.check_in_date - interval '5 days',
    NULL,
    false,
    NULL
FROM booking_dates bd;

-- 5. Заполнение Payments (20 записей)
WITH bookings_with_info AS (
    SELECT 
        b."Id" as booking_id,
        b."CheckInDate",
        b."CheckOutDate",
        r."PricePerNight",
        ROW_NUMBER() OVER (ORDER BY b."CreatedAt") as rn
    FROM "Bookings" b
    JOIN "Rooms" r ON b."RoomId" = r."Id"
)
INSERT INTO "Payments" ("Id", "TotalAmount", "PaymentDate", "PaymentMethod", "Status", "BookingId", "CreatedAt", "UpdatedAt", "IsDeleted", "DeletedAt")
SELECT 
    gen_random_uuid(),
    bwi."PricePerNight" * 5,
    bwi."CheckInDate" - interval '1 day',
    CASE 
        WHEN bwi.rn % 3 = 0 THEN 2
        WHEN bwi.rn % 3 = 1 THEN 3
        ELSE 1
    END,
    1,
    bwi.booking_id,
    bwi."CheckInDate" - interval '1 day',
    NULL,
    false,
    NULL
FROM bookings_with_info bwi;

-- 6. Заполнение Reviews (20 записей)
WITH bookings_list AS (
    SELECT 
        "Id" as booking_id,
        ROW_NUMBER() OVER (ORDER BY "CreatedAt") as rn
    FROM "Bookings"
)
INSERT INTO "Reviews" ("Id", "Rating", "Text", "BookingId", "CreatedAt", "UpdatedAt", "IsDeleted", "DeletedAt")
SELECT 
    gen_random_uuid(),
    CASE 
        WHEN bl.rn % 20 = 0 THEN 3
        WHEN bl.rn % 7 = 0 THEN 4
        ELSE 5
    END,
    CASE 
        WHEN bl.rn % 20 = 0 THEN 'Номер стандартный, ничего особенного. Завтрак скудный.'
        WHEN bl.rn % 7 = 0 THEN 'Хороший номер, чисто и уютно. Небольшой минус - слабый WiFi.'
        WHEN bl.rn % 5 = 0 THEN 'Панорамный вид захватывает дух! Рекомендую.'
        WHEN bl.rn % 4 = 0 THEN 'Джакузи просто супер! Отдых на высшем уровне.'
        WHEN bl.rn % 3 = 0 THEN 'Прекрасный отдых! Номер с балконом - это то что нужно.'
        ELSE 'Отличный номер, прекрасный вид из окна. Персонал очень внимательный.'
    END,
    bl.booking_id,
    (SELECT "CheckOutDate" FROM "Bookings" WHERE "Id" = bl.booking_id) + interval '1 day',
    NULL,
    false,
    NULL
FROM bookings_list bl;
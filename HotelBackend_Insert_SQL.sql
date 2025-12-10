DELETE FROM "Reservations";
DELETE FROM "Users";
DELETE FROM "Rooms";
DELETE FROM "UserTypes";

select * from "Reservations";
select * from "Users";
select * from "Rooms";
select * from "UserTypes";

SELECT table_name 
FROM information_schema.tables 
WHERE table_schema = 'public'
ORDER BY table_name;
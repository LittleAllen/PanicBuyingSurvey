docker-compose up -d

連進db connetion在(config/secret.json)

照小allen提供的sql initial
https://hub.docker.com/r/fzr7425/my_postgre_db

壓測jmx放在testcase資料夾直接取用

若要在local跑要先起db
docker run --name MyPostgre -e POSTGRES_USER=sa -e POSTGRES_PASSWORD=pass123 -p 5432:5432 -v 'panicbuyingsurvey_db-data:/var/lib/postgresql/data' --rm -d  fzr7425/my_postgre_db


## Database 的建立

```shell
docker pull postgres
```

```shell
docker run --name MyPostgre -e POSTGRES_USER=sa -e POSTGRES_PASSWORD=pass123 -p 5432:5432 -d postgres
```

透過IDE 連線PostgreSQL Server: 127.0.0.1:5432
UserID: sa
Password: pass123

### initial SQL
```sql
-- create table
CREATE TABLE products
(
    id          int NOT NULL PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    name        VARCHAR(50),
    price       money,
    stock       int,
    create_time timestamptz(3),
    update_time timestamptz(3)
);

-- insert data
INSERT INTO products(name, price, stock, create_time, update_time)
VALUES ('iPhone', 20000, 50, now(), now());

-- create proc
CREATE PROCEDURE sp_UpdateStock(in idKey integer, in stockValue integer)
    LANGUAGE SQL as $$
UPDATE products
SET stock       = stockValue,
    update_time = now()
WHERE id = idKey
    $$;

-- execute proc
call sp_UpdateStock(1,50);

```


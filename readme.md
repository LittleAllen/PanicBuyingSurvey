docker-compose up -d

連進db connetion在(config/secret.json)

照小allen提供的sql initial
https://hub.docker.com/r/fzr7425/my_postgre_db

壓測jmx放在testcase資料夾直接取用

若要在local跑要先起db
docker run --name MyPostgre -e POSTGRES_USER=sa -e POSTGRES_PASSWORD=pass123 -p 5432:5432 -v 'panicbuyingsurvey_db-data:/var/lib/postgresql/data' --rm -d  fzr7425/my_postgre_db

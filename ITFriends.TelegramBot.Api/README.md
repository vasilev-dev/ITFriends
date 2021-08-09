# ITFriends.TelegramBot #

## Локальный запуск
1. Скачать https://dashboard.ngrok.com/get-started/setup
4. Запустить `ngrok http 8443`
5. Вставить ключ `1qNoELDNqCrvTHa8B7N3kdu1vw8_5jzpUhYCT6z6zDYg2BMAm`
    1. Eсли ngrok выдаст предупреждение о том, что ключ уже используется, то необходимо...
    2. Зарегестироваться на https://ngrok.com/
    3. Скопировать выданный ключ и использовать его
6. Скопировать сгенерированный адрес *(Forwarding - https)*
7. Встать адрес в конфиг *appsettings.Development.json -> BotSettings -> WebHookUrl*

## Локальный запуск в Docker
> Может не работать, т.к. иногда ngrok пишет, что порт 4040 занят, хотя он свободен.

1. Перейти в *ITFriends.DevOps -> docker-compose.yml -> ngrok*
2. Запустить контейнер и перейти в CLI через Docker Desktop
4. Запустить `ngrok http 8443`
    1. Eсли ngrok выдаст предупреждение о том, что ключ уже используется, то необходимо...
    2. Зарегестироваться на https://ngrok.com/
    3. Заменить в *ITFriends.DevOps -> docker-compose.yml -> ngrok -> environment -> NGROK_AUTH* ключ на выданный
6. Скопировать сгенерированный адрес *(Forwarding - https)*
7. Встать адрес в конфиг *appsettings.Development.json -> BotSettings -> WebHookUrl*

## Deploy
Адрес сервиса должен распологаться на __443, 80, 88__ или __8443__ порту, т.к telegram поддерживает только эти порты.

Для настройки Webhook'а необходимо вставить в *appsettings.json -> BotSettings -> WebHookUrl* адрес сервиса.
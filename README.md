## Задание:

Используя фреймворк ASP.Net Core, создайте набор бекенд сервисов, через которые пользователи смогут регистрироваться и обмениваться сообщениями. Приложение должно содержать минимум два микросервиса:

- **API регистрации пользователей**
- **API получения и обмена сообщениями**

Организуйте доступ к сервисам через API Gateway.

### Подробное описание задания:

Создайте проект ASP.Net Core и приложение(я) почтового ящика. Создайте два микросервера: работа с пользователями и работа с сообщениями.

#### Сервис пользователей

Обладает следующим функционалом:

- Сервис должен уметь регистрировать пользователей, предоставляя соответствующий метод API. В качестве имени пользователя нужно использовать email, в качестве пароля - произвольную строку.
- Сервис должен уметь добавлять и аутентифицировать пользователей на основе асимметричного шифрования RSA.
- Методы:
  - Добавить администратора (первый пользователь в системе)
  - Добавить пользователя (обязательна проверка на дублирующиеся имена/адреса)
  - Получить список пользователей
  - Удалить пользователя (доступ только у администратора), администратор не может удалить сам себя
  - Метод возвращающий ID пользователя при обращении с JWT-токеном (для проверки работоспособности API)
- База данных:
  - Пользователи
  - Роли
- ID пользователя, который будет использоваться в сервисе обмена сообщениями, должен быть добавлен в JWT(Claim) для использования в качестве ID в сервисе сообщений.

#### Сервис сообщений

Предназначен для обмена сообщениями между авторизованными пользователями. Обладает следующим функционалом:

- Сервис позволяет отправлять сообщения от имени авторизованного пользователя.
- Сервис позволяет получать сообщения, предназначенные для авторизованного пользователя.
- Сервис помечает полученные сообщения, чтобы избежать повторной отправки.
- Сервис получает только те сообщения, которые не помечены как полученные.
- Методы:
  - Получить сообщения
  - Отправить сообщение
- База данных:
  - Сообщения
  - Пользователи:
    - Авторизуются через сервис пользователей
    - Clam’ы JWT должны содержать Guid пользователя

 ####   Тестирование
Напишите Unit-тесты для вашего решения.
#### Postman
Для тестирования вашего решения подготовьте коллекцию в Postman
покрывающую все API-вызовы обоих сервисов.

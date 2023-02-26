# ADVERT Portal - _ASP.NET Core application_

[Zobacz demo aplikacji](http://advertportal.slajan.com.pl)


## Typ aplikacji - portal ogłoszeniowy

- publiczny dostęp do przeglądania ogłoszeń wraz z filtrowaniem wyników

- dla zalogowanych użytkowników:
- dodawanie / edytowanie / usuwanie własnych ogłoszeń wraz ze zdjęciami oferowanych usług i produktów
- możliwość oznaczania ogłoszeń innych użytkowników jako obserwowane
- wyświetlanie tylko własnych ogłoszeń
- wyświetlanie tylko obserwowanych ogłoszeń

 - logowanie z użyciem Nazwy Użytkownika (zamiast e-mail)
- polecenia dostęne dla zalogowanego użytkownika umieszczone pod przyciskiem z jego nazwą

## Tech 

- UI - Bootstrap 5 - [Bootswatch SANDSTONE Theme](https://bootswatch.com/sandstone/)
- zastosowanie kontrolki [CKEDITOR 5](https://ckeditor.com/ckeditor-5/) do wprowadzania opisu produktu/usługi w formacie html
- zastosowanie biblioteki [HTML.Sanitizer](https://github.com/mganss/HtmlSanitizer) w celu ochrony przed atakami XSS
- dane przechowywane w bazie danych MSSQL

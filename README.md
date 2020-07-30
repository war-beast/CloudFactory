# Тестовое задание CloudFactory на бэк-разработчика .NET (уровень: simple)
## Легенда
Представим себе систему, обрабатывающую входящий http трафик, запрашивая
бэкэнд и отдающая его результат в ответе. Бэкэндом у нас являются файлы в каком-то
каталоге (content root например). API выглядит так: GET /?filename=xxx.txt ,где xxx.txt -
произвольное имя файла, которое и выдается в Reponse.
Чтение файла у нас “долгое” - т.е. вычитка файла занимает продолжительное время.
## Задача
Реализовать корректную работу сценария при запросе файла:
* если файл уже читается другим “вызывающим”, то ожидаем пока тот зачитает
файл и отдаем результат чтения активного читателя (двойной “перечитки”
файла НЕ производим).
* если же файл не читается никем иным, то читаем его и эмулируем задержку в 2
секунды (эмуляция “долгого чтения”).

[Ссылка на вакансию](https://career.habr.com/vacancies/1000047532)

***

Выполнено за 6 часов.
На клиенской стороне можно запускать несколько запросов на сервер одновременно. Количество запросов можно менять - добавлять и удалять строки.

Подумал чуть и добавил ещё 2 варианта кеширования. В этом репозитории есть 2 домолнительные ветки, кроме master:
1. **1-using-memorycache** - вариант с кешированием загруженных файлов в MemoryCache.
2. **2-using-redis** - вариант с кешированием в распределённом хранилище Redis (для проверки работы должен быть установлен сервер Redis на компьютере).

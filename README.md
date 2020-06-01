# analytics_microservice
A microservice API indended to serve as an analytics module for my personal projects.

--Documentation --


	--How to post data--

Client script can be found in the ClientScript folder of the main directory.
Reference client script in any HTML document to have client side code send api session data.



	--How to request data --

Most api requests for data are made using http://127.0.0.1:5000/reports/for_domain/...

Extensions for ..:

../on_month/summary
../on_month/raw

../on_date/summary
../on_date/raw

../between_dates/summary
../between_dates/raw



-Example of requesting a report on user activity for the month using postman:

create a get request at the url http://127.0.0.1:5000/reports/for_domain/on_month/raw

create a raw json body with the following object:
{
	"domain":"127.0.0.1:8000",
	"year":2020,
	"month": 6
}

Click send to see all user sessions for the month in a potentially massive list.

A summary report can also be requested using http://127.0.0.1:5000/reports/for_domain/on_month/summary

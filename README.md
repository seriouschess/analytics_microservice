# analytics_microservice
A microservice API indended to serve as an analytics module for my personal projects.

--Documentation --


	--How to post data--

Add this line of code to the end of the body of any HTML page you wish to track
<script content-type="text/javascript" src="http://analytics.siteleaves.com/cdn/logscript"></script>

	--How to request data --

Most api requests for data are made using http://analytics.siteleaves.com/reports/for_domain/...

Extensions for ..:

../on_month/summary
../on_month/raw

../on_date/summary
../on_date/raw

../between_dates/summary
../between_dates/raw



-Example of requesting a report on user activity for the month using postman:

create a get request at the url http://analytics.siteleaves.com/reports/for_domain/on_month/raw

create a raw json body with the following object:
```yaml
{
  "domain":"www.example.com",
  "year":2020,
  "month": 6
}
```

Click send to see all user sessions for the month in a potentially massive list.

A summary report can also be requested using http://analytics.siteleaves.com/reports/for_domain/on_month/summary

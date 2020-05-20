
const express = require("express"); //using/import statement
const app = express();

app.use(express.static( __dirname + '/static' ));

app.use(express.urlencoded({extended: true}));
//app.use(express.json());

app.all("*", (req, res,next) => { //angular routes
    res.sendFile(path.resolve("index.html"))
  });

app.listen(8000, () => console.log("listening on port 8000"));

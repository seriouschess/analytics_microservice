async function read(){
    let options = {
        "method": "GET",
        "headers": {
            'Content-Type': 'application/json'
        },
        "mode": "cors"
    }

    let response = await fetch("http://127.0.0.1:5000/analytics/read", options);
    const json = await response.json();
    for(var x=0; x < json.length; x++){
        document.getElementById("analytics_information").innerHTML += JSON.stringify(json[x], null, 2);
    }
}

async function createSession(){
    // let data =  {
    //     "session_id": 0, //inconsequential
    //     "time_on_homepage": time
    // }

    let options = {
        "method" : "POST",
        "headers": {
            'Content-Type': 'application/json'
        },
        "mode": "cors",
        "body":JSON.stringify(session) //session is a global object
    }

    let response = await fetch("http://127.0.0.1:5000/analytics/create", options);
    const json = await response.json();
    console.log( json );
    session.session_id = json.session_id;
    session.token = json.token;
}

async function updateSession(){
    // let data =  {
    //     "session_id": 0, //inconsequential
    //     "time_on_homepage": time
    // }

    let options = {
        "method" : "POST",
        "headers": {
            'Content-Type': 'application/json'
        },
        "mode": "cors",
        "body":JSON.stringify(session)
    }

    let response = await fetch("http://127.0.0.1:5000/analytics/update", options);
    const json = await response.json();
    console.log( json );
}

function update(){
    time += 1;
    session.time_on_homepage = time;
    if(time >= session_interval){
        console.log("session_interval: "+session_interval+" time:"+time);
        session_interval *= 2;
        updateSession();
    }
}

// --- main ---

var time = 0;
var session_interval = 5;
var session = {
    "session_id": 0, //inconsequential
    "token": "duaiosfbol", //unsigned default
    "time_on_homepage": time,
    "url": window.location.href
}

createSession();
setInterval(() => {update()}, 1000);
setTimeout(() => {read()}, 7000);







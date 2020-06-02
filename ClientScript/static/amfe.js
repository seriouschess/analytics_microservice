async function createSession(){

    let options = {
        "method" : "POST",
        "headers": {
            'Content-Type': 'application/json'
        },
        "mode": "cors",
        "body":JSON.stringify(session) //session is a global object
    }

    let response = await fetch("http://analytics.siteleaves.com/storage/create", options);
    const json = await response.json();
    console.log( json );
    session.session_id = json.session_id;
    session.token = json.token;
}

async function updateSession(){

    let options = {
        "method" : "POST",
        "headers": {
            'Content-Type': 'application/json'
        },
        "mode": "cors",
        "body":JSON.stringify(session)
    }

    let response = await fetch("http://analytics.siteleaves.com/storage/update", options);
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







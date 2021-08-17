var tdConnection = require("tedious").Connection;



let config = {
    server: '127.0.0.1',  //update me
    authentication: {
        type: 'default',
        options: {
            userName: 'sa', //update me
            password: 'Mydev#333'  //update me
        }
    },
    options: {
        // If you are on Microsoft Azure, you need encryption:
        encrypt: true,
        database: 'graphdemo'  //update me
    }
};


var connection = new tdConnection(config);
connection.on('connect', function (err) {
    // If no error, then good to proceed.
    console.log("Connected");
});


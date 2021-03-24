'use strict';
var iothub = require('azure-iothub');
var Client = require('azure-iothub').Client;
var connectionString = 'HostName=sweng.azure-devices.net;SharedAccessKeyName=service;SharedAccessKey=NTkLL9oPk7Gu8RrfadVc7+DoO9HnBvDWv9x311tv7nM=';
var registry = iothub.Registry.fromConnectionString(connectionString);
var deviceId = 'AZ-c89346886016';


// Connect to the service-side endpoint on your IoT hub.
var client = Client.fromConnectionString(connectionString);

// Set the direct method name, payload, and timeout values
var methodParams = {
    methodName: 'setCurrent',
    payload: 10, // Number of seconds.
    responseTimeoutInSeconds: 30
  };

// Call the direct method on your device using the defined parameters.
client.invokeDeviceMethod(deviceId, methodParams, function (err, result) {
if (err) {
    console.error('Failed to invoke method \'' + methodParams.methodName + '\': ' + err.message);
} else {
    console.log('Response from ' + methodParams.methodName + ' on ' + deviceId + ':');
    console.log(JSON.stringify(result, null, 2));
}
});

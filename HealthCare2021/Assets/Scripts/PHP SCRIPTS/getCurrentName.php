<?php

$con = mysqli_connect('192.168.0.100', 'pantera1_alsivic', '81(wE(pzS9', 'pantera1_alsivic');

// Check if the connection happened
if (mysqli_connect_errno()) {
    echo "1: connection failed"; // error code #1 = connection failed
    exit();
}

$phoneNumber = $_POST["phoneNumber"];

// Check if the phoneNumber exists
$phoneNumberCheckQuery = "SELECT name, patientID FROM users WHERE phoneNumber='" . $phoneNumber . "';";

$phoneNumberCheck = mysqli_query($con, $phoneNumberCheckQuery) or die("2: phoneNumber check query failed"); // error code #2 - phoneNumber check query failed

if (mysqli_num_rows($phoneNumberCheck) > 0) {
    $userInfo = mysqli_fetch_assoc($phoneNumberCheck);
    echo json_encode($userInfo); // Return a well-structured JSON object with both name and patientID
} else {
    // phoneNumber doesn't exist, handle accordingly
    echo "6: phoneNumber does not exist"; // error code #6 - phoneNumber does not exist
}

mysqli_close($con);

?>

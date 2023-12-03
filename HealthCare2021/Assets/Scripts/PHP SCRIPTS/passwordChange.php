<?php

$con = mysqli_connect('192.168.0.100', 'pantera1_alsivic', '81(wE(pzS9', 'pantera1_alsivic');

// Check if the connection happened
if (mysqli_connect_errno()) {
    echo "1: connection failed"; // error code #1 = connection failed
    exit();
}

$password = $_POST["password"];
$phoneNumber = $_POST["phoneNumber"];

// Check if the phoneNumber exists
$phoneNumberCheckQuery = "SELECT phoneNumber FROM users WHERE phoneNumber='" . $phoneNumber . "';";

$phoneNumberCheck = mysqli_query($con, $phoneNumberCheckQuery) or die("2: phoneNumber check query failed"); // error code #2 - phoneNumber check query failed

if (mysqli_num_rows($phoneNumberCheck) > 0) 
{
    $salt = "\$5\$rounds=5000\$" . "steamedhams" . $phoneNumber . "\$";
    $hash = crypt($password, $salt);
    $updateUserQuery = "UPDATE users SET hash='" . $hash . "', salt='" . $salt . "' WHERE phoneNumber='" . $phoneNumber . "';";
    $result = mysqli_query($con, $updateUserQuery);

    if ($result) {
        echo "0"; // Update successful
    } else {
        echo "5: update failed"; // error code #5 - update failed
    }
} else {
    // phoneNumber doesn't exist, handle accordingly
    echo "6: phoneNumber does not exist"; // error code #6 - phoneNumber does not exist
}

mysqli_close($con);

?>
<?php

$con = mysqli_connect('192.168.0.100', 'pantera1_alsivic', '81(wE(pzS9', 'pantera1_alsivic');

// Check if the connection happened
if (mysqli_connect_errno()) {
    echo "1: connection failed"; // error code #1 = connection failed
    exit();
}

$cnp = $_POST["cnp"];

// Check if the cnp exists
$cnpCheckQuery = "SELECT name, doctorID FROM doctors WHERE cnp='" . $cnp . "';";

$cnpCheck = mysqli_query($con, $cnpCheckQuery) or die("2: cnp check query failed"); // error code #2 - cnp check query failed

if (mysqli_num_rows($cnpCheck) > 0) {
    $userInfo = mysqli_fetch_assoc($cnpCheck);
    echo json_encode($userInfo); // Return a well-structured JSON object with both name and doctorID
} else {
    // cnp doesn't exist, handle accordingly
    echo "6: cnp does not exist"; // error code #6 - cnp does not exist
}

mysqli_close($con);

?>

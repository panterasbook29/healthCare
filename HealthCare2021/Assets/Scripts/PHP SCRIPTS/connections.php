<?php

$con = mysqli_connect('192.168.0.100', 'pantera1_alsivic', '81(wE(pzS9', 'pantera1_alsivic');

// Check if the connection happened
if (mysqli_connect_errno()) {
    echo "1: connection failed"; // error code #1 = connection failed
    exit();
}

$doctor_id = $_POST['doctor_id'];
$patient_id = $_POST['patient_id'];

// Insert connection into the database
$query = "INSERT INTO `connections` (`doctorID`, `patientID`) VALUES ('" . $doctor_id . "', '" . $patient_id . "')";

if (mysqli_query($con, $query)) {
    echo "Connection added successfully";
} else {
    echo "Error adding connection: " . mysqli_error($con);
}

mysqli_close($con);
?>

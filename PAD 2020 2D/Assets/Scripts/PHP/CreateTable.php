<?php

	$con = mysqli_connect("oege.ie.hva.nl", "baasdr", "l#aEkJ7cojymzj", "zbaasdr", "3306");
	if (mysqli_connect_errno()) {
		echo "1";
		exit();
	}
	
	$query = "CREATE TABLE IF NOT EXISTS Player (username varchar(16), score int, PRIMARY KEY(username))";
	if ($stmt = $con->prepare($query)) {
		$stmt->execute();
		$stmt->close();
	}
	$highscore = "CREATE TABLE IF NOT EXISTS Highscore (username varchar(16), highscore int, PRIMARY KEY(username, highscore), FOREIGN KEY(username) REFERENCES Player(username))";
	if ($stmt = $con->prepare($highscore)) {
		$stmt->execute();
		$stmt->close();
	}
	
	$con->close();
?>
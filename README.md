# What is rageOS?
The ultimate open source rageMP GTA V multiplayer server build with GTA-N .NET Core bridge and rageMP server files.

# Why?
There are many Developers out there, developing cool resources for all kind of multiplayer servers for GTA V.
The goal of this project is to provide a place for all developers to contribute their sources to the community to build the most advanced, most exciting, most configurable rageMP server resource available.

# How?
This repository will be setup to hold the basic needs for the resources. It includes a well structured database schema which makes all thinkable scenarios of ingame features possible.
Contributers can provide pull requests with their fully documented sources to help build the server we have all dreamed of.

# Rules
* All contributions have to be tested by the community before merging
* All contributions must be fully documented (functions, entities, etc.)
* All contributions are licenced under the same licence as rageOS
* All contributions have to have configuruation files for behavior control (enabling or disabling features, setting prices, amounts etc.)

# Installation

Create a file in the bridge folder with the following content or add the missing lines:

```
<?xml version="1.0"?>
<config xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <resource src="rageOS" />
</config>
```

* Install the database in your MySQL or MariaDB Server
* Download the resources and put them into the bridge/resources folder

# FreeCMS
### It's a simple headless content management system for education purposes. Some features may not work correctly, besides that please follow the instructions for correct operation.

### The source code will not work unless you create a database that fits in the project's needs. You can browse and test the project from my website, which is built on the existing database.

Website for testing: http://erayertas.com/freecms

Note: The project used and tested with Swagger.

## Beginning
Firstly, you need to register and log in for get privileges to create or edit contents. There is no need to log in to use get requests. Only post, put and delete requests does.

After register, put your account informations to authentication section and get your JWT token. Paste it to textbox by clicking authorize button on top right corner of the Swagger UI.

## Important: To List Ordered Contents
### Ordering Field Syntax:  
To get ordered contents correctly, you must put 'asc' or 'desc' after field name that you want to sort on orderField from GET /api/v1/contents. For example 'year asc' sorts by ascend year value if there's any variable which named 'year' on the field. 'year desc' does same but reversed.

### Examples with photos
#### Input:
![alt text](https://i.ibb.co/72sPRHF/Screen-Shot-2021-11-08-at-9-31-14-AM.png)

#### Output:
![alt text](https://i.ibb.co/87FgVDt/Screen-Shot-2021-11-08-at-9-25-40-AM.png)

#### Input:
![alt text](https://i.ibb.co/D559846/Screen-Shot-2021-11-08-at-9-31-01-AM.png)

#### Output:
![alt text](https://i.ibb.co/9WZS9NP/Screen-Shot-2021-11-08-at-9-27-59-AM.png)

## Usage of 'GET /api/v1/contents/{contentType}'
contentType: searches by contentType, can't get null value.

orderField: Orders by value on the field, orders by id number if left blank. Usage explained at above. 

offset: Offsets the content list when getting specified contents. Example; if there are 4 contents, starts sorting from specified number on offset. Gets '0' value when left blank.

pageSize: Lists the contents as many as desired.

## Usage of 'POST /api/v1/content/{contentType}'
There is contentType and request body in this block. Request body must be in json format. 

### Last Notes: I didn't need to explain for the other blocks because they have the same concept.

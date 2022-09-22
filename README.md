## Note: No longer updated/supported. I keep for archive purposes at the moment.

#### I don't have any motivation to continue this project. 
#### Feel free to fork if it'll matter.

## Why website that for testing is not working ?
#### I was using this website for my personal portfolio, after that I decided to shut down because of it is not personally necessary anymore.
#### I'll be updated the link with new source for testing soon.

# FreeCMS

### FreeCMS is a simple headless content management system. 
### Please follow the instructions for correct operation.

##### The source code will not work unless you create a database that fits in the project's needs. You can find database creation codes which required to create the database in `tables.sql`. Also you can browse and test the project from my website, which is built on the existing database.

Website for testing: http://erayertas.com/freecms

## Requirements
* .NET SDK
* Microsoft SQL Server

## Beginning
You need to register and log in for get privileges to create or edit contents. There is no need to log in to use get requests.

After register, put your account informations to authentication section and get your JWT token. Paste it to textbox by clicking authorize button on top right corner of the Swagger UI.

## Usage of Post Contents
Post request to the /api/v1/contents/{contentType} endpoint, the field must be in json format at should be sent via body.
```
{
  {
    "Category": "Movies",
    "Author": "Freeman527",
    "Creation Date": "05/29/2015"
  }
}
```

## Usage of Get Contents (Multiple contents)
### Get request for /api/v1/contents/{contentType}

contentType: searches by content type which exists in database, can't get null value.

orderField: Orders by value on the field, orders by id number when left blank. Usage explained at below. 

offset: Offsets the content list when getting specified contents. Example; if there are 4 contents, starts sorting from specified number on offset. Gets '0' value when left blank.

pageSize: Lists the contents as many as desired.

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

# Post Office Back-End

This is a web application for managing parcels and letters in a post office. The application allows you to create shipments, add bags to the shipments, and finalize the shipments. 
The home page displays a list of all created shipments. Clicking on a shipment shows a list of bags in the shipment. Add date page displays different forms for saving
different kinds of data to the database.

## Requirements
- .NET Core
- MSSQL (Express) Database
- Entity Framework

## Getting Started

1) Clone repository
2) To run the API, navigate to the API project directory and run:
     dotnet run --urls="https://localhost:5002"

## API Endpoints

- `POST /api/Shipment/Initial`: Creates a new shipment.
- `POST /api/Bag`: Adds a bag to a shipment.
- `POST /api/Parcel`: Adds a Parcel to the bag.
- `POST /api/Shipment/Final?shipmentNumber=`: Finalizes a shipment.
- `POST /api/Bag/Letters`: Adds letters to shipment.
- `GET /api/Shipments/`: Get all created shipments.

## Data Description

The application uses the following data structures:

- **Parcel**: A parcel has a unique number, recipient name, destination country, weight, and price.
- **Bag with Parcels**: A bag can contain multiple parcels. Each bag has a unique number.
- **Bag with Letters**: A bag can contain multiple letters. Each bag has a unique number, count of letters, weight, and price.
- **Shipment**: A shipment can contain multiple bags. Each shipment has a unique number, airport, flight number, and flight date.

## Testing

Unit tests are located in the `PostOfficeTests` project.

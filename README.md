# Makaan

![Makaan Real Estate Project](/Frontend/Makaan.WebUI/wwwroot/ProjectImages/Slayt1.png)

## About the Project

Makaan is a modern and user-friendly real estate platform. This platform enables real estate agents to easily share property listings and allows users to search and browse real estate listings using various criteria.

The project includes the following features:
- Filtering real estate listings by city
- Searching by rental or for-sale status
- Filtering by property categories (Apartment, Villa, Detached House, etc.)
- User-friendly interface and detailed property information
- Management panel for real estate agents

Developed using microservice architecture, the project provides high scalability and modularity.

## Project Architecture

The Makaan project is built on a microservice architecture:

```
Makaan/
│
├── ApiGateway/             # API Gateway service
│
├── IdentityServer/         # Authentication and authorization service
│
├── Frontends/
│   ├── DtoLayer/           # Data transfer objects
│   └── UI/                 # User interface
│
└── Services/
    ├── Catalog/            # Real estate catalog service
    ├── Comment/            # Comment service
    ├── Message/            # Messaging service
    └── Notification/       # Notification service
```

## Filtering Features

![Filtering Options](/Frontend/Makaan.WebUI/wwwroot/ProjectImages/filter1.png)

Users can easily search by different property categories. The platform offers filtering options in the following categories:
- Apartment
- Villa
- Detached House
- Office
- Building
- Mountain House
- Commercial Property
- Garage

The number of available listings is shown for each category, and users can select the most suitable property type according to their needs.

## Property Listing

![Property List](/Frontend/Makaan.WebUI/wwwroot/ProjectImages/List1.png)

On the property listing page, users can clearly view rental and for-sale options. Detailed information is provided for each listing, such as:
- Price information
- Location information
- Square footage
- Number of rooms
- Number of bathrooms
- Property type (Detached House, Villa, Apartment, etc.)
- High-quality photos

## Admin Panel

![Admin Panel](/Frontend/Makaan.WebUI/wwwroot/ProjectImages/Admin1.png)

The admin panel provides the ability to monitor a lot data of the real estate platform in real-time. Thanks to this dashboard, which is developed using SignalR technology, important data can be tracked in a useful way, such as:
- Lowest and highest rental values
- Lowest and highest for-sale values
- Property distribution by categories
- Real-time visitor statistics
- Newly added listings
- User interactions 

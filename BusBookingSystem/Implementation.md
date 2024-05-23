# API Usage Flow for the Bus Booking System

Here is how the API will be used by admins and users for adding buses, booking seats, and making payments.

## Admin Adds Buses and Routes

1. **Admin Authentication**
    - **Endpoint**: `POST /api/admin/login`
    - **Description**: Admin logs in to the system to manage buses and routes.
    - **Request Body**:
      ```json
      {
        "email": "admin@example.com",
        "password": "adminPassword"
      }
      ```

2. **Add a Route**
    - **Endpoint**: `POST /api/routes`
    - **Description**: Admin adds a new route.
    - **Request Body**:
      ```json
      {
        "source": "City A",
        "destination": "City B",
        "departureTime": "2024-05-25T08:00:00",
        "arrivalTime": "2024-05-25T12:00:00"
      }
      ```

3. **Add a Bus**
    - **Endpoint**: `POST /api/buses`
    - **Description**: Admin adds a new bus to a specific route.
    - **Request Body**:
      ```json
      {
        "busNumber": "AB1234",
        "capacity": 40,
        "routeId": 1
      }
      ```

## User Books Seats

1. **User Registration**
    - **Endpoint**: `POST /api/users/register`
    - **Description**: User registers an account.
    - **Request Body**:
      ```json
      {
        "userName": "john_doe",
        "email": "john@example.com",
        "password": "password123"
      }
      ```

2. **User Login**
    - **Endpoint**: `POST /api/users/login`
    - **Description**: User logs in to the system to book seats.
    - **Request Body**:
      ```json
      {
        "email": "john@example.com",
        "password": "password123"
      }
      ```

3. **View Available Buses**
    - **Endpoint**: `GET /api/buses`
    - **Description**: User retrieves a list of available buses with routes.

4. **Book a Seat**
    - **Endpoint**: `POST /api/bookings`
    - **Description**: User books a seat on a selected bus.
    - **Request Body**:
      ```json
      {
        "userId": 1,
        "busId": 1,
        "bookingDate": "2024-05-24",
        "seatNumber": 12
      }
      ```

## User Makes Payment

1. **Make a Payment**
    - **Endpoint**: `POST /api/payments`
    - **Description**: User makes a payment for the booking.
    - **Request Body**:
      ```json
      {
        "bookingId": 1,
        "amount": 100.00,
        "paymentDate": "2024-05-24",
        "paymentMethod": "CreditCard",
        "status": "Completed"
      }
      ```

2. **Apply a Coupon**
    - **Endpoint**: `POST /api/coupons/apply`
    - **Description**: User applies a coupon to get a discount.
    - **Request Body**:
      ```json
      {
        "couponCode": "DISCOUNT10",
        "bookingId": 1
      }
      ```

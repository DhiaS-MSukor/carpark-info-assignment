namespace CarparkInfoAssignmentDhia.Dtos;

public record class CsvDto(
    string car_park_no,
    string address,
    string x_coord,
    string y_coord,
    string car_park_type,
    string type_of_parking_system,
    string short_term_parking,
    string free_parking,
    string night_parking,
    string car_park_decks,
    string gantry_height,
    string car_park_basement
    );
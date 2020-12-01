use reqwest::header::ACCEPT;
use reqwest::header::COOKIE;
use std::fs;

pub fn fetch_input(year: u32, day: u32) -> String {
    let token = fs::read_to_string("temp/token.txt").unwrap();

    let path = format!("temp/input_{}_{}.txt", year, day);
    let cached = fs::read_to_string(path.to_owned());
    if cached.is_ok() {
        return cached.unwrap();
    }

    let client = reqwest::blocking::Client::new();
    let res = client
        .get(&format!("https://adventofcode.com/{}/day/{}/input", year, day))
        .header(COOKIE, format!("session={}", token))
        .header(ACCEPT, "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8")
        .send()
        .unwrap()
        .text()
        .unwrap(); 

    fs::write(path, res.to_owned()).unwrap();

    return res;
}
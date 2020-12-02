mod util;
mod problem;
use crate::problem::Problem;

mod day1;
mod day2;

fn main() -> Result<(), Box<dyn std::error::Error>> {
    
    let problem = day2::Day2();

    println!();
    let input = util::fetch_input(problem.year(), problem.day());

    let example1 = problem.solve_first(problem.example_first().to_string());
    println!("Example First: {:#?}", example1);

    let answer1 = problem.solve_first(input.to_owned());
    println!("Real First: {:#?}", answer1);

    println!();

    let example2 = problem.solve_second(problem.example_first().to_string());
    println!("Example Second: {:#?}", example2);

    let answer2 = problem.solve_second(input);
    println!("Real Second: {:#?}", answer2);

    Ok(())
}
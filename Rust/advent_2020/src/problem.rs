pub trait Problem {
    fn year(&self) -> u32;
    fn day(&self) -> u32;

    fn example_first(&self) -> &str;
    fn solve_first(&self, input: String) -> String;

    fn example_second(&self) -> &str;
    fn solve_second(&self, input: String) -> String;
}
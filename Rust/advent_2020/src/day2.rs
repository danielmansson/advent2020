use crate::problem::Problem;

struct Input {
    min: usize,
    max: usize,
    letter: char,
    password: String,
}

fn transform(input: String) -> Vec<Input> {
    return input
    .split("\n")
    .filter(|i| !i.is_empty())
    .map(|line| {
        let parts: Vec<&str> = line.split(" ").collect();
        let range: Vec<&str> = parts[0].split("-").collect();

        return Input{
            min: range[0].parse::<usize>().unwrap(),
            max: range[1].parse::<usize>().unwrap(),
            letter: parts[1].chars().nth(0).unwrap(),
            password: parts[2].to_string()
        };
    })
    .collect();
}

pub struct Day2();

impl Problem for Day2 {
    fn year(&self) -> u32 { 2020 }
    fn day(&self) -> u32 { 2 }

    fn example_first(&self) -> &str
    {
"1-3 a: abcde
1-3 b: cdefg
2-9 c: ccccccccc"
    }

    fn solve_first(&self, input: String) -> String {
        let data = transform(input);

        return data
            .iter()
            .filter(|i| {
                let c = i.password.matches(i.letter).count();
                return c >= i.min && c <= i.max;
            })
            .count()
            .to_string();
    }

    fn example_second(&self) -> &str { 
        return self.example_first();
     }

    fn solve_second(&self, input: String) -> String{
        let data = transform(input);

        return data
            .iter()
            .filter(|i| {
                let i1 = i.min - 1;
                let i2 = i.max - 1;

                return (i.password.chars().nth(i1).unwrap() == i.letter) 
                    ^ (i.password.chars().nth(i2).unwrap() == i.letter);
            })
            .count()
            .to_string();
    }
}

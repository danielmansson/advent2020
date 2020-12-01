use crate::problem::Problem;

fn transform(input: String) -> Vec<i32> {
    return input
    .split("\n")
    .filter(|i| !i.is_empty())
    .map(|i| i.parse::<i32>().unwrap())
    .collect();
}

pub struct Day1();

impl Problem for Day1 {
    fn year(&self) -> u32 { 2020 }
    fn day(&self) -> u32 { 1 }

    fn example_first(&self) -> &str
    {
"1721
979
366
299
675
1456"
    }

    fn solve_first(&self, input: String) -> String {
        let data = transform(input);

        for i in 0..data.len() {
            for j in i + 1..data.len() {
                if data[i] + data[j] == 2020 {
                    return (data[i] * data[j]).to_string();
                }
            }
        }
        
        return 0.to_string();
    }

    fn example_second(&self) -> &str { 
        return self.example_first();
     }

    fn solve_second(&self, input: String) -> String{
        let data = transform(input);

        for i in 0..data.len() {
            for j in i + 1..data.len() {
                for k in j + 1..data.len() {
                    if data[i] + data[j] + data[k] == 2020 {
                        return (data[i] * data[j] * data[k]).to_string();
                    }
                }
            }
        }
        
        return 0.to_string();
    }
}

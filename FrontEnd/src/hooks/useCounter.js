import { useState } from "react"


const reg = new RegExp('^-?[0-9]+$');
export const useCounter = (initialValue = 10) => {


    const [counter, setCounter] = useState(initialValue);

    const increment = (step = 1) => {
        
        setCounter((current) => {
            if (current.toString().match(reg))
                return current + step;
            else
                return 0 + step;
        });
    }
    const decrement = (step = 1) => {
        // if(counter===1)return;
        setCounter((current) => {
            if (current.toString().match(reg))
                return current - step;
            else
                return 0 - step;
        });
    }

    const reset = () => {
        setCounter(initialValue);
    }

    const refresh = (value) => {
        if (value.toString().match(reg))
            setCounter(parseInt(value));
        else if (value.length == 0)
            setCounter("");
    }



    return {
        counter,
        increment,
        decrement,
        reset,
        refresh
    }
}
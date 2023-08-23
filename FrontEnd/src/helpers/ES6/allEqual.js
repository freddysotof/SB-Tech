export const allEqual = (arr, prop, value) => arr.every(obj => {
    if (!prop)
        return obj === arr[0];
    if (!value)
        return obj[prop] === arr[0][prop]
    return obj[prop] === arr[0][prop][value];
});
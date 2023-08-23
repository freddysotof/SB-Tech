export const onFormValuesChange = (_, allFields) => {
  const obj = {};
  allFields.forEach(field => {
    const { name, value } = field;
    obj[name[0]] = value;
  })
  return obj;
}
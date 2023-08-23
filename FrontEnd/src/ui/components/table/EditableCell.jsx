import { Form, Input, InputNumber } from "antd";
import { useState } from "react";

export const EditableCell = ({
    editing,
    dataIndex,
    title,
    inputType,
    editableRender,
    record,
    index,
    children,
    rules,
    ...restProps
}) => {
    // const inputNode = inputType === 'number' ? <InputNumber /> : <Input />;

    return (
        <td {...restProps}>
            {editing ? (
                <Form.Item
                    initialValue={''}
                    name={dataIndex}
                    style={{
                        margin: 0,
                    }}
                    rules={rules ?? [
                        {
                            required: true,
                            message: `Please Input ${title}!`
                        }
                    ]}
                >
                    {
                        editableRender ?
                            editableRender(record[dataIndex], record)
                            : <Input />}
                </Form.Item>
            ) : (
                children
            )}
        </td>
    );
};

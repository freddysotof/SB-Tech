import { Form } from 'antd';
import React from 'react'
import { EditableContext } from './EditableContext';

export const EditableProvider = ({ children }) => {
    const [form] = Form.useForm();
    return (
        <Form form={form} component={false}>
            <EditableContext.Provider value={form}>
                {children}
            </EditableContext.Provider>
        </Form>
    )
}

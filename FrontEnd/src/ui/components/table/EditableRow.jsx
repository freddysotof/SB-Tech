import { Form } from 'antd';
import React from 'react'
import { EditableContext, EditableProvider } from '../../../context';
import { EditableCell } from './EditableCell';


export const EditableRow = ({ ...props }) => {
    return (
        <EditableProvider>
            {
                props.children.map(child => ({ ...child }))
            }
        </EditableProvider>
    );
};

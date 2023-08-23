import { Button, Form, Popconfirm, Space, Table, Typography } from 'antd'
import { QuestionCircleFilled} from "@ant-design/icons";
import React, { useContext, useEffect, useState } from 'react'
import { EditableCell } from '../../../ui';
import { useTheme, useUiStore } from '../../../hooks';


const { Text } = Typography;
export const EditableTable = ({ columns, data, onSave, onDelete }) => {
  const [editingKey, setEditingKey] = useState('');
  const [records, setRecords] = useState(data)
  const [form] = Form.useForm();
  const {
    colorPrimary,
    colorDanger
  } = useTheme()

  const {
    showWarningMessage,
    showConfirm
  } = useUiStore();

  const isEditing = (record) => record.key === editingKey;

  const edit = (record) => {
    form.setFieldsValue(record);
    setEditingKey(record.key);
  };

  const cancel = () => {
    if (editingKey === undefined)
      setRecords([...records].filter(record => record.key !== undefined))
    setEditingKey('');

  };

  const save = async (key) => {
    try {
      const row = await form.validateFields();
      await onSave({ key, ...row });
      setEditingKey('');
    } catch (errInfo) {
      console.log('Validate Failed:', errInfo);
    }
  };

  const onDeleteRecord = async (record)=>{
    await showConfirm('Eliminar producto', 'Esta seguro que desea eliminar este producto?', <QuestionCircleFilled />,()=> onDelete(record));
  }

  const onAddRecord = () => {
    const hasNewRecord = records.find(record => record.key === undefined);
    if (hasNewRecord)
      return showWarningMessage('Debe finalizar la edicion del registro activo');
    setRecords([{ id: undefined, barcode: undefined, name: undefined, description: undefined, unitPrice: 0 }, ...records])
    setEditingKey(undefined);
    form.resetFields();
  }

  const actionOptionsColumn = {
    title: (<Space>
      <Text>Acciones</Text>
      <Button onClick={onAddRecord}>Nuevo</Button>
    </Space>),
    dataIndex: 'operation',
    render: (_, record) => {
      const editable = isEditing(record);
      return editable ? (
        <span>
          <Typography.Link
            onClick={() => save(record.key)}
            style={{
              color: 'black',
              marginRight: 8,
            }}
          >
            Save
          </Typography.Link>
          <Popconfirm  title="Sure to cancel?" onConfirm={cancel}>
            <a style={{color:colorDanger}}> Cancel</a>
          </Popconfirm>
        </span>
      ) : (
        <Space>
          <Typography.Link disabled={editingKey !== ''} onClick={() => edit(record)}
            style={{ color: colorPrimary,  marginRight: 8, }}
          >
            Edit
          </Typography.Link>
          <Typography.Link disabled={editingKey !== ''} onClick={() => onDeleteRecord(record)}
            style={{ color: colorDanger }}
          >
            Delete
          </Typography.Link>
        </Space>

      );
    },
  }

  const mergedColumns = columns.concat([actionOptionsColumn]).map((col) => {
    if (!col.editable) {
      return col;
    }
    return {
      ...col,
      onCell: (record) => {
        return ({
          record,
          dataIndex: col.dataIndex,
          title: col.title,
          editing: isEditing(record),
          editableRender: col.editableRender
        })
      },
    };
  });

  useEffect(() => {
    setRecords(data?.map(record => {
      return {
        key: record.id,
        ...record
      }
    }));
  }, [data])

  return (
    <Form form={form} component={false}>
      <Table
        components={{
          body: {
            // row: (props) => <EditableRow {...props} />,
            cell: (props) => <EditableCell {...props} />
          },
        }}
        bordered
        dataSource={records}
        columns={mergedColumns}
        rowClassName="editable-row"
        pagination={{
          // onChange: cancel,
        }}
      />
    </Form>
  )
}

interface Props {
    label: string,
    name: string,
    onChangeHandler: (e: React.ChangeEvent<HTMLInputElement>) => void,
    value?: any
}

export default function ReuseableTextInput({ label, name, onChangeHandler, value }: Props) {
    return (
        <div className="mb-5">
            <label
                className="mb-3 block text-base font-medium text-[#07074D]">
                {label}
            </label>
            <input
                onChange={onChangeHandler}
                value={value}
                type="text"
                name={name}
                placeholder="Enter The New Title"
                className="w-full rounded-md border border-[#e0e0e0] bg-white py-3 px-6 text-base font-medium text-[#6B7280] outline-none focus:border-[#6A64F1] focus:shadow-md"
            />
        </div>
    )
}

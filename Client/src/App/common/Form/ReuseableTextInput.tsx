interface Props {
  label: string
  name: string
  onChangeHandler: (e: React.ChangeEvent<HTMLInputElement>) => void
  value?: any
  nameSvg?: string // tambahkan prop nameSvg
}

export default function ReuseableTextInput({
  label,
  name,
  onChangeHandler,
  value,
  nameSvg,
}: Props) {
  // gunakan prop nameSvg
  return (
    <div className="mb-5">
      <label className="mb-3 block text-base font-medium text-[#5c5c5c]">
        {label}
      </label>
      {/* menambahkan kondisi untuk mengecek apakah nameSvg telah diberikan */}
      {nameSvg && (
        <i className={`mt-3 ml-1 absolute ${nameSvg}`}></i>

      )}
      <input
        onChange={onChangeHandler}
        value={value}
        type="text"
        name={name}
        placeholder="Enter The New Title"
        className="w-full border-b-2 border-[#949494] bg-inherit py-3 px-8 text-base text-[#5c5c5c] font-medium outline-none focus:border-[#5c5c5c]"
      />
    </div>
  )
}

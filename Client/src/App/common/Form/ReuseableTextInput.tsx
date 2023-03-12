interface Props {
  label: string
  name: string
  type: string // tambah prop type
  onChangeHandler: (e: React.ChangeEvent<HTMLInputElement>) => void
  value?: any
  nameSvg?: string // tambahkan prop nameSvg
  placeholder?: string // tambah prop placeholder (optional)
}

export default function ReuseableTextInput({
  label,
  name,
  type, // taruh prop type
  onChangeHandler,
  value,
  nameSvg,
  placeholder // taruh prop placeholder
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
        type={type} // pakai prop type disini
        name={name}
        placeholder={placeholder} // pakai prop placeholder disini
        className="w-full border-b-2 border-[#949494] bg-inherit py-3 px-8 text-base text-[#5c5c5c] font-medium outline-none focus:border-[#5c5c5c] placeholder:italic placeholder:text-sm placeholder-slate-400" // tambah styling placeholder
      />
    </div>
  )
}

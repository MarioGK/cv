<script lang='ts'>
  import { slugify } from "../utils/string";

  export let title: string
  export let name = ''

  if (!name) name = slugify(title)
</script>

<a href={`#${name}`} {name}>{ title }</a>

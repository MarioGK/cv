<script type="ts">
  import type { CurriculumSocialType } from "./Curriculum.type";

  export let social: CurriculumSocialType[] | undefined = undefined

  const urlToText = (url: string) => url.replace(/^http[s]?:\/\/|\/\//ig, '')
</script>

{#if social && social.length > 0}
<div class="secondary-info ml-md-auto mt-2">
  <ul class="resume-social list-unstyled">
    {#each social as item (item.url)}
    <li class="mb-2">
      <a href={ item.url } class="d-flex">
        <span class="fa-container text-center mr-2">
          <i class="fab { item.icon || '' } fa-fw">
          </i>
        </span>
        { urlToText(item.text || item.url) }
      </a>
    </li>
    {/each}
  </ul>
</div><!--//secondary-info-->
{/if}
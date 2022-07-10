<script type='ts'>
  import { slide } from 'svelte/transition'

  import { toList } from "../../utils/string";

  import type { CurriculumPositionType } from "./Curriculum.type";
  import Toggle from "./Toggle.svelte"

  export let position: CurriculumPositionType
  const technologies = toList(position.technologies)

  let visible = true
</script>

<article class="resume-timeline-item position-relative pb-3">
  
  <div class="resume-timeline-item-header mb-2">
    <div on:click={() => visible = !visible} class="d-flex flex-column flex-md-row">
      <h3 class="resume-position-title font-weight-bold mb-1">{ position.title }
        <Toggle {visible}/>
        <!-- <i class="resume-award-icon fas fa-toggle-on" data-fa-transform="shrink-2"></i> -->
      </h3>
      <div class="resume-company-name ml-auto">{ position.company }</div>
    </div><!--//row-->
    <div class="resume-position-time">{ position.time }</div>
  </div><!--//resume-timeline-item-header-->
  {#if visible}
  <div class="resume-timeline-item-desc" transition:slide>
    {#if position.text }<p>{ position.text }</p>{/if}
    {#each position.sections as section}
    <h4 class="resume-timeline-item-desc-heading font-weight-bold">{ section.title }:</h4>
    {#if section.text }<p>{ section.text }</p>{/if}
    <ul>
      {#each toList(section.items) as item}
      <li>{ item }</li>
      {/each}
    </ul>
    {/each}
    {#if technologies && technologies.length > 1}
    <h4 class="resume-timeline-item-desc-heading font-weight-bold">Technologies used:</h4>
    <ul class="list-inline">
      {#each technologies as tech}
      <li class="list-inline-item"><span class="badge badge-primary badge-pill">{ tech }</span></li>
      {/each}
    </ul>
    {/if}
  </div><!--//resume-timeline-item-desc-->
  {/if}
</article><!--//resume-timeline-item-->